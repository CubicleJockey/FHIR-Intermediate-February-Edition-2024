using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi_u2_lib;
using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Newtonsoft.Json;

namespace fhirclient_dotnet
{
    public class FetchIPS : BasePatientSearch
    {
        /*
         * MedicationStatement IPS Documentation: https://hl7.org/fhir/uv/ips/StructureDefinition-MedicationStatement-uv-ips.html
         * 
         * Immunization IPS Documentation: https://hl7.org/fhir/uv/ips/StructureDefinition-Immunization-uv-ips.html
         */

        /// <summary>
        /// LIONC code for IPS (International Patient Summary)
        /// </summary>
        //private readonly string LoincIpsCode = "http://loinc.org|60591-5";
        private readonly string LoincIpsCode = "60591-5";

        public string GetIPSMedications(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }

            using var fhirClient = new FhirClient(serverEndPoint);

            var patientSummary = GetPatientSummary(serverEndPoint, patient);
            var medicationStatements= GetPatientSummaryMedicationStatements(patientSummary);

            var hasIps = HasIps(patientSummary);
            
            if (!hasIps) { return "Error:No_IPS"; }

            var response = new StringBuilder();
            foreach (var statement in medicationStatements)
            {
                var status = statement.Status;
                var date = (statement.Effective as Period)?.Start;

                //No medication so this is an actual object not ReferenceResource
                if (statement.Medication is CodeableConcept noRefCoding)
                {
                    if (noRefCoding.Coding.First() is { } coding)
                    {
                        //TODO: Double check forum about the status being Unknown in JSON response but expecting Active
                        response.Append($"{status}|{date}|{coding.Code}:{coding.Display}\n");
                        //response.Append($"Active|{date}|{coding.Code}:{coding.Display}\n");
                    }
                }

                //ReferenceResource Object
                if (statement.Medication is ResourceReference medicationReference)
                {
                    var medication = GetMedicationFromReference(patientSummary, medicationReference);
                    if (medication?.Code is { } codeConcept)
                    {
                        if (codeConcept.Coding.First() is { } coding)
                        {
                            response.Append($"{status}|{date}|{coding.Code}:{coding.Display}\n");
                        }
                    }
                }
            }

            return response.ToString();
        }

        public string GetIPSImmunizations(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            const string NOIMMUNIZATIONS = "Error:IPS_No_Immunizations";
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }

            var patientSummary = GetPatientSummary(serverEndPoint, patient);
            var immunizations = GetImmunizations(patientSummary);
            
            var response = new StringBuilder();

            if (immunizations == default || immunizations.Count == 0)
            {
                return NOIMMUNIZATIONS;
            }

            if (immunizations.All(immunization => immunization.Status == Immunization.ImmunizationStatusCodes.NotDone))
            {
                return NOIMMUNIZATIONS;
            }

            foreach (var immunization in immunizations)
            {
                var status = immunization.Status;
                var occurrenceDate = (immunization.Occurrence as FhirDateTime)?.Value;
                var coding = immunization.VaccineCode.Coding.First();
                
                response.Append($"{status}|{occurrenceDate}|{coding.Code}:{coding.Display}\n");
            }
            
            return response.ToString();
        }

        #region Helper Methods

        private static Bundle GetPatientSummary(string serverEndPoint, Resource patient)
        {
            using var fhirClient = new FhirClient(serverEndPoint);

            var operationUri = new Uri($"{serverEndPoint}/Patient/{patient.Id}");
            const string operationName = "summary"; //$summary
            var bundle = (Bundle)fhirClient.InstanceOperation(operationUri, operationName);
            return bundle;
        }
        
        //Example: http://hl7-ips-server.org:8080/fhir/Patient/1/$summary
        private static IList<MedicationStatement> GetPatientSummaryMedicationStatements(Bundle patientSummary)
        {
            if (patientSummary.Entry.Count == 0) { return default; }
            var medicationStatements =
                patientSummary.Entry
                             .Where(entry => entry.Resource is MedicationStatement)
                             .Select(entry => entry.Resource as MedicationStatement)
                             .ToArray();
            
            return medicationStatements;
        }

        private static IList<Immunization> GetImmunizations(Bundle patientSummary)
        {
            if(patientSummary.Entry.Count == 0) { return default; }

            var immunizations =
                patientSummary.Entry
                    .Where(entry => entry.Resource is Immunization)
                    .Select(entry => entry.Resource as Immunization)
                    .ToArray();
            
            return immunizations;
        }
        
        private static IList<AllergyIntolerance> GetAllergyIntolerances(Bundle patientSummary)
        {
            if (patientSummary.Entry.Count == 0) { return default; }
            var allergies =
                patientSummary.Entry
                    .Where(entry => entry.Resource is AllergyIntolerance)
                    .Select(entry => entry.Resource as AllergyIntolerance)
                    .ToArray();

            return allergies;
        }

        private static IList<Condition> GetConditions(Bundle patientSummary)
        {
            if (patientSummary.Entry.Count == 0) { return default; }

            var conditions =
                patientSummary.Entry
                    .Where(entry => entry.Resource is Condition)
                    .Select(entry => entry.Resource as Condition)
                    .ToArray();

            return conditions;
        }
        
        private static Medication GetMedicationFromReference(Bundle patientSummary, ResourceReference reference)
        {
            var medicationId = reference.Reference;
            var medicationResource = patientSummary.Entry.FirstOrDefault(entry => entry.FullUrl == medicationId)?.Resource;

            if (medicationResource is Medication medication) { return medication; }
            
            return default;
        }


        /// <summary>
        /// Per comments in the Forum from Rik Smithies:
        /// No IPS means (ALL 3 conditions true):
        ///   1. MedicationStatement with status = unknown and medicationCodeableConcept = "No information about medication" 
        ///   2. AllergyIntolerance with clinicalStatus = active and code = no-allergy-info OR "No information about allergies" 
        ///   3. Condition with clinicalStatus = active and code = no-problem-info OR "No information about problems" and
        /// </summary>
        private static bool HasIps(Bundle patientSummary)
        {
            var medicationStatements = GetPatientSummaryMedicationStatements(patientSummary);
            var allergies = GetAllergyIntolerances(patientSummary);
            var conditions = GetConditions(patientSummary);

            if (medicationStatements.Count == 1
                && allergies.Count == 1
                && conditions.Count == 1)
            {
                var statement = medicationStatements.Single();
                var allergy = allergies.Single();
                var condition = conditions.Single();

                var isMedicationUnknown = statement.Status == MedicationStatement.MedicationStatusCodes.Unknown;
                var isMedicationNoInformation = !(statement.Medication is ResourceReference) && ((CodeableConcept)statement.Medication).Coding.First().Code == "no-medication-info";

                var isAllergyClinicalStatusActive = allergy.ClinicalStatus.Coding.First().Code == "Active";
                var isAllergyNoInformation = allergy.Code.Coding.First().Code == "no-allergy-info";

                var isConditionClinicalStatusActive = condition.ClinicalStatus.Coding.First().Code == "Active";
                var isConditionNoInformation = condition.Code.Coding.First().Code == "no-problem-info";

                return isMedicationUnknown
                       && isMedicationNoInformation
                       && isAllergyClinicalStatusActive
                       && isAllergyNoInformation
                       && isConditionClinicalStatusActive
                       && isConditionNoInformation;
            }

            return true;
        }
        
        #endregion Helper Methods
    }
}
