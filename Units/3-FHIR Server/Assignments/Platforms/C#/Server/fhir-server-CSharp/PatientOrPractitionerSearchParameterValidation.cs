using fhir_server_entity_model;
using fhir_server_sharedservices;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace fhir_server_CSharp
{
    public static class PatientOrPractitionerSearchParameterValidation
    {
        public static bool ValidateSearchParams(HttpListenerRequest request, ref bool hardIdSearch, out DomainResource operation, out List<LegacyFilter> criteria, bool usePractitioner = false)
        {
            var resource = nameof(ResourceType.Patient);
            if (usePractitioner)
            {
                resource = nameof(ResourceType.Practitioner);
            }
            
            operation = null;
            criteria = new List<LegacyFilter>();

            var searchParamId = string.Empty;
            var rtnValue = true;

            var resourceBeingSearched = request.Url.AbsolutePath.Replace(FhirServerConfig.FHIRServerUrl, string.Empty);

            if (!string.IsNullOrWhiteSpace(resourceBeingSearched) && resourceBeingSearched.Contains("/"))
            {
                searchParamId = resourceBeingSearched.Substring(resourceBeingSearched.IndexOf("/"));
                if (searchParamId.StartsWith("/"))
                {
                    resourceBeingSearched = resourceBeingSearched.Substring(0, resourceBeingSearched.IndexOf("/"));
                    searchParamId = searchParamId.Substring(1);
                }
            }
            else if (!string.IsNullOrWhiteSpace(resourceBeingSearched) && resourceBeingSearched.Contains("?"))
            {
                searchParamId = resourceBeingSearched.Substring(resourceBeingSearched.IndexOf("?"));
                if (searchParamId.StartsWith("?"))
                {
                    resourceBeingSearched = resourceBeingSearched.Substring(0, resourceBeingSearched.IndexOf("?"));
                }
            }

            //As of now only GET is supported. Sorry about that as I did not have time to complete POST operation.
            //For the time being, please use the data inserted into the database manually. I inserted some data manually into DB.
            if (!request.HttpMethod.Trim().ToUpper().Equals("GET"))
            {
                rtnValue = false;
                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.MethodNotAllowed;
                operation = Utilz.getErrorOperationOutcome(
                    $"Unsupported http method '{request.HttpMethod}' for {resource} resource- Server knows how to handle: [GET] only for Patient resource");
            }
            else if (string.IsNullOrWhiteSpace(resourceBeingSearched))
            {
                rtnValue = false;
                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                operation = Utilz.getErrorOperationOutcome(
                    $"Unknown resource type '{resourceBeingSearched}' - Server knows how to handle: [Patient, Practitioner, MedicationRequest]");
            }
            else if (!resourceBeingSearched.Equals(nameof(ResourceType.Patient), StringComparison.Ordinal)
                     && !resourceBeingSearched.Equals(nameof(ResourceType.Practitioner), StringComparison.Ordinal))
            {
                rtnValue = false;
                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                operation = Utilz.getErrorOperationOutcome(
                    $"Unknown resource type '{resourceBeingSearched}' - Server knows how to handle: [Patient, Practitioner, MedicationRequest]");
            }
            else if ((resourceBeingSearched.Equals(nameof(ResourceType.Patient), StringComparison.Ordinal) || resourceBeingSearched.Equals(nameof(ResourceType.Practitioner), StringComparison.Ordinal))
                     && request.QueryString is { Count: 0 } && !string.IsNullOrWhiteSpace(searchParamId))
            {
                if (!long.TryParse(searchParamId, out _))
                {
                    rtnValue = false;
                    Program.HttpStatusCodeForResponse = (int)HttpStatusCode.NotFound;
                    operation = Utilz.getErrorOperationOutcome(
                        $"Resource {resourceBeingSearched}/{searchParamId} is not known");
                }
                else
                {
                    var sc = new LegacyFilter
                    {
                        criteria = LegacyFilter.field.id,
                        value = searchParamId.ToString()
                    };
                    criteria.Add(sc);
                    hardIdSearch = true;
                    return rtnValue;
                }
            }
            else
            {


                if (request.QueryString != null && request.QueryString.Count > 0)
                {
                    foreach (var param in request.QueryString)
                    {
                        if (param.ToString().Equals("_id", StringComparison.OrdinalIgnoreCase))
                        {
                            //check the case now;
                            if (!param.ToString().Equals("_id", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }

                            var sc = new LegacyFilter
                            {
                                criteria = LegacyFilter.field._id,
                                value = request.QueryString[param.ToString()]
                            };
                            rtnValue = true;
                            criteria.Add(sc);
                        }
                        else if (param.ToString().Equals("identifier", StringComparison.OrdinalIgnoreCase))
                        {
                            //check the case now;
                            if (!param.ToString().Equals("identifier", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }

                            var search_system = string.Empty;
                            var search_type = string.Empty;
                            var search_value = string.Empty;

                            var SystemAndValue = request.QueryString[param.ToString()].Split("|", StringSplitOptions.RemoveEmptyEntries);
                            
                            if (SystemAndValue != null && SystemAndValue.Length > 1)
                            {
                                search_system = SystemAndValue[0];
                                
                                if (usePractitioner && !search_system.Contains("NPI"))
                                {
                                    rtnValue = false;
                                    Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                    operation = Utilz.getErrorOperationOutcome(
                                        $" HTTP {Program.HttpStatusCodeForResponse} Bad Request: The person you requested is not a practitioner - Lacks a NPI identifier");
                                    break;
                                }
                                
                                search_type = SharedServices.GetSystemTypeMapping().SystemMap
                                    .Where(e => e.System.Equals(search_system, StringComparison.Ordinal))
                                    .Select(e => e.Type).FirstOrDefault();
                                search_value = SystemAndValue[1];

                                if (string.IsNullOrWhiteSpace(search_type))
                                {
                                    rtnValue = false;
                                    Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                    operation = Utilz.getErrorOperationOutcome($"Identifier '{search_system}' is not a valid system for {resource} resource.");
                                    break;
                                }
                            }
                            else
                            {
                                search_value = request.QueryString[param.ToString()];
                            }

                            if (!string.IsNullOrWhiteSpace(search_type) && search_type.Equals("ID"))
                            {
                            }
                            else
                            {
                                var sc = new LegacyFilter
                                {
                                    criteria = LegacyFilter.field.identifier,
                                    value = $"{search_system}|{search_value}"
                                };
                                criteria.Add(sc);
                                rtnValue = true;
                            }
                        }
                        else if (param.ToString().Equals("family", StringComparison.OrdinalIgnoreCase))
                        {
                            //check the case now;
                            if (!param.ToString().Equals("family", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }
                            else
                            {
                                var sc = new LegacyFilter
                                {
                                    criteria = LegacyFilter.field.family,
                                    value = request.QueryString[param.ToString()]
                                };
                                criteria.Add(sc);
                                rtnValue = true;
                            }

                            ;
                        }
                        else if (param.ToString().Equals("name", StringComparison.OrdinalIgnoreCase))
                        {
                            //check the case now;
                            if (!param.ToString().Equals("name", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }

                            var sc = new LegacyFilter
                            {
                                criteria = LegacyFilter.field.name,
                                value = request.QueryString[param.ToString()]
                            };
                            criteria.Add(sc);
                            rtnValue = true;

                        }
                        else if (param.ToString().Equals("birthdate", StringComparison.OrdinalIgnoreCase))
                        {
                            //check the case now;
                            if (!param.ToString().Equals("birthdate", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }

                            var sc = new LegacyFilter
                            {
                                criteria = LegacyFilter.field.birthdate,
                                value = request.QueryString[param.ToString()]
                            };
                            criteria.Add(sc);
                            rtnValue = true;

                        }
                        else if (param.ToString().Equals("gender", StringComparison.OrdinalIgnoreCase))
                        {
                            //check the case now;
                            if (!param.ToString().Equals("gender", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }

                            var sc = new LegacyFilter
                            {
                                criteria = LegacyFilter.field.gender,
                                value = request.QueryString[param.ToString()]
                            };
                            criteria.Add(sc);
                            rtnValue = true;

                        }
                        else if (param.ToString().Equals("email", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!param.ToString().Equals("email", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }
                            
                            var searchCriteria = new LegacyFilter
                            {
                                criteria = LegacyFilter.field.email,
                                value = request.QueryString[param.ToString()]
                            };
                            criteria.Add(searchCriteria);
                            rtnValue = true;

                        }
                        else if (param.ToString().Equals("telecom", StringComparison.OrdinalIgnoreCase)) 
                        {
                            if (!param.ToString().Equals("telecom", StringComparison.Ordinal))
                            {
                                rtnValue = false;
                                Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                                break;
                            }    
                            
                            
                            
                            var item = request.QueryString[param.ToString()];

                            if (!item.Contains('|')) //No System
                            {
                                if (IsValidEmail(item))
                                {
                                    var searchCriteria = new LegacyFilter
                                    {
                                        criteria = LegacyFilter.field.email,
                                        value = item
                                    };
                                    criteria.Add(searchCriteria);
                                }
                                else
                                {
                                    rtnValue = false;
                                    Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                                    operation = Utilz.getErrorOperationOutcome($"HTTP {(int)HttpStatusCode.NotImplemented} Not Implemented: The underlying server only handles email addresses for the patients, thus search by system=phone is not implemented");
                                }
                            }
                            else
                            {

                                var parts = item.Split('|');

                                switch (parts[0].ToLower())
                                {
                                    case "email":
                                        var searchCriteria = new LegacyFilter
                                        {
                                            criteria = LegacyFilter.field.email,
                                            value = parts[1]
                                        };
                                        criteria.Add(searchCriteria);
                                        break;
                                    case "phone":
                                        rtnValue = false;
                                        Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;

                                        var types = usePractitioner ? "practitioners" : "patients";
                                        var message =  $"HTTP {(int)HttpStatusCode.NotImplemented} Not Implemented: The underlying server only handles email addresses for the {types}, thus search by system=phone is not implemented";
                                        
                                        operation = Utilz.getErrorOperationOutcome(message);
                                        break;
                                }
                            }
                        }
                        else
                        {
                            rtnValue = false;
                            Program.HttpStatusCodeForResponse = (int)HttpStatusCode.BadRequest;
                            operation = Utilz.getErrorOperationOutcome(ParamErrorMessage(param.ToString()));
                            break;
                        }
                    }
                }
            }

            return rtnValue;
        }


        private static string ParamErrorMessage(string param) => $"Unknown search parameter \"{param}\". Value search parameters for this search are: [_id, birthdate, email, telecom, family, gender, name, identifier]";

        private static bool IsValidEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch (FormatException) { return false; }
        }
    }
}
