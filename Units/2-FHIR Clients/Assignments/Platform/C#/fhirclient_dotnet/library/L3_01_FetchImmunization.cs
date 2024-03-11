using System.Linq;
using System.Text;
using fi_u2_lib;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class FetchImmunization : BasePatientSearch
    {
        public string GetImmunizations(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }

            using var fhirClient = new FhirClient(serverEndPoint);
            var immunizationSearchResults = fhirClient.Search<Immunization>(new []{ $"patient={patient.Id}" });
            if (immunizationSearchResults.Entry.Count == 0)
            {
                return "Error:No_Immunizations";
            }

            var response = new StringBuilder();
            foreach (var entry in immunizationSearchResults.Entry)
            {
                var immunization = (Immunization)entry.Resource;

                var status = immunization.Status;
                var coding = immunization.VaccineCode.Coding.First();
                var administrationDate = (FhirDateTime)immunization.Occurrence;

                response.Append($"{status}|{coding.Code}:{coding.Display}|{administrationDate.Value}\n");
            }

            return response.ToString();

        }
    }
}
