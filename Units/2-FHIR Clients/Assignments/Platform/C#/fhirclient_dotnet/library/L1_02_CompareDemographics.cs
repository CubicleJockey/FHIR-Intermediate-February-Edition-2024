using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class CompareDemographics
    {
        public string GetDemographicComparison(string serverEndPoint, string identifierSystem, string identifierValue,
            string family, string given, string gender, string birthDate)
        {
            var patient = new Patient();

            using var client = new FhirClient(serverEndPoint);
            var patientSearchBundle = client.Search<Patient>(new[] { $"identifier={identifierSystem}|{identifierValue}"});
            if (patientSearchBundle.Entry.Count > 0)
            {
                patient = (Patient)patientSearchBundle.Entry[0].Resource;
            }
            else { return "Error:Patient_Not_Found"; }

            var demographics = new StringBuilder();

            var name = patient.Name.First();
            var fhirFamily = name.Family;
            var givenParts = name.Given.ToArray();

            var fhirGiven = string.Join(" ", givenParts);
            
            var fhirGender = patient.Gender.ToString()?.ToUpper();
            var fhirBirthDate = patient.BirthDate.ToString();

            gender = gender.ToUpper();
            
            demographics.Append($"{{family}}|{family}|{fhirFamily}|{IsDifferent(family, fhirFamily)}\n");
            demographics.Append($"{{given}}|{given}|{fhirGiven}|{IsDifferent(given, fhirGiven)}\n");
            demographics.Append($"{{gender}}|{gender}|{fhirGender}|{IsDifferent(gender, fhirGender)}\n");
            demographics.Append($"{{birthDate}}|{birthDate}|{fhirBirthDate}|{IsDifferent(birthDate, fhirBirthDate)}\n");
            
            
            return demographics.ToString();
        }

        private static string IsDifferent(string local, string fhir) => local == fhir ? "{green}" : "{red}";
    }
}
