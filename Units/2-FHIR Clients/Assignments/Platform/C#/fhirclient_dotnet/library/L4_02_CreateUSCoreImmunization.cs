using System.Collections.Generic;
using fi_u2_lib;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using static System.Net.WebRequestMethods;

namespace fhirclient_dotnet
{
    //Documentation: https://www.hl7.org/fhir/immunization.html
    public class CreateUSCoreImm : BasePatientSearch
    {
        public string CreateUSCoreR4Immunization
        (
            string serverEndpoint,
            string patientIdentifierSystem,
            string patientIdentifierValue,
            string immunizationStatusCode,
            string immunizationDateTime,
            string productCVXCode,
            string productCVXDisplay,
            string reasonCode
        )
        {
            var patient = SearchPatient(serverEndpoint, patientIdentifierSystem, patientIdentifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }

            var immunization = new Immunization
            {
                Patient = GetResourceReference(patient),
                Status = GetImmunizationStatusCodes(immunizationStatusCode),
                Occurrence = new FhirDateTime(immunizationDateTime),
                VaccineCode = new CodeableConcept("http://hl7.org/fhir/sid/cvx", productCVXCode, productCVXDisplay, default),
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = $"<div xmlns=\"http://www.w3.org/1999/xhtml\">{productCVXCode}:{productCVXDisplay}</div>"
}
            };

            //NOTE: https://fhir-ru.github.io/immunization-definitions.html#Immunization.statusReason
            if (!string.IsNullOrWhiteSpace(reasonCode))
            {
                immunization.ReasonCode = new List<CodeableConcept>
                {
                    new CodeableConcept
                    {
                        Coding = new List<Coding>
                        {
                            new Coding
                            {
                                System = "http://hl7.org/fhir/sid/cvx",
                                Code = reasonCode
                            }
                        }
                    }
                };
            }

            //using var fhirClient = new FhirClient(serverEndpoint);
            //var createdObservation = fhirClient.Create(Immunization);

            var serializer = new FhirJsonSerializer();
            var json = serializer.SerializeToString(immunization);

            return json;
        }

        #region Helper Methods

        private static Immunization.ImmunizationStatusCodes GetImmunizationStatusCodes(string status)
        {
            var convertedStatus = status.ToLower() switch
            {
                "enteredinerror" => Immunization.ImmunizationStatusCodes.EnteredInError,
                "notdone" => Immunization.ImmunizationStatusCodes.NotDone,
                "completed" => Immunization.ImmunizationStatusCodes.Completed,
                _ => Immunization.ImmunizationStatusCodes.NotDone
            };

            return convertedStatus;
        }

        private static ResourceReference GetResourceReference(Resource resource)
        {
            var reference = new ResourceReference
            {
                Reference = $"{resource.TypeName}/{resource.Id}"
            };

            return reference;
        }

        #endregion Helper Methods
    }
}
