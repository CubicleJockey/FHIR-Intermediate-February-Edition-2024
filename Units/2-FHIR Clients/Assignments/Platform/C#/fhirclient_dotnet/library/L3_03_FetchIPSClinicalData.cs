using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi_u2_lib;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

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

            var (medicationStatements, medications) = GetPatientSummaryMedicationStatements(serverEndPoint, patient);

            if (medicationStatements.Count == 0 || medications.Count == 0)
            {
                return "Error:No_IPS";
            }
            
            var response = new StringBuilder();
            for (var i = 0; i < medicationStatements.Count; i++)
            {
                var statement = medicationStatements[i];
                var status = statement.Status;
                var date = ((Period)statement.Effective).Start;

                var medication = medications[i];

                var code = medication.Code.Coding.First();
                
                response.Append($"{status}|{date}|{code.Code}:{code.Display}\n");
            }

            return response.ToString();
        }

        public string GetIPSImmunizations(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }

            
            
            var aux = "";
            return aux;
        }

        #region Helper Methods

        //Example: http://hl7-ips-server.org:8080/fhir/Patient/1/$summary
        private static (IList<MedicationStatement>, IList<Medication>) GetPatientSummaryMedicationStatements(string serverEndPoint, Patient patient)
        {
            using var fhirClient = new FhirClient(serverEndPoint);
        
            var operationUri = new Uri($"{serverEndPoint}/Patient/{patient.Id}");
            var operationName = "summary"; //$summary
            var summaryResult = (Bundle)fhirClient.InstanceOperation(operationUri, operationName);

            if (summaryResult.Entry.Count == 0) { return default; }


            var medicationStatements = new List<MedicationStatement>();
            var medications = new List<Medication>();
            foreach (var entry in summaryResult.Entry)
            {
                switch (entry.Resource.TypeName)
                {
                    case "MedicationStatement":
                    {
                        var medicationStatement = (MedicationStatement)entry.Resource;
                        medicationStatements.Add(medicationStatement);
                        break;
                    }
                    case "Medication":
                    {
                        var medication = (Medication)entry.Resource;
                    
                        medications.Add(medication);
                        break;
                    }
                }
            }
            
            return (medicationStatements, medications);
        }
        
        // private static Medication GetMedicationByReference(string serverEndPoint, ResourceReference resourceReference)
        // {
        //     using var fhirClient = new FhirClient(serverEndPoint);
        //     var uri = $"{serverEndPoint}/Medication/{resourceReference.Reference}";
        //     var medication = fhirClient.Read<Medication>(new Uri(uri));
        //    
        //     return medication;
        // }
        
        #endregion Helper Methods
    }
}
