using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class FetchEthnicity
    {
        public string GetEthnicity(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return "Error:Patient_Not_Found"; }
            if (patient.Extension.Count == 0) { return "Error:No_us-core-ethnicity_Extension"; }

            var aux = "";
            return aux;

        }

        /* Documentation: https://www.hl7.org/fhir/patient.html */
        private static Patient SearchPatient(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            Patient patient;
            using var client = new FhirClient(serverEndPoint);
            var patientResults = client.Search<Patient>(new[] { $"identifier={identifierSystem}|{identifierValue}" });

            if (patientResults.Entry.Count > 0)
            {
                patient = (Patient)patientResults.Entry[0].Resource;
            }
            else { patient = default; }

            return patient;
        }
    }
}
