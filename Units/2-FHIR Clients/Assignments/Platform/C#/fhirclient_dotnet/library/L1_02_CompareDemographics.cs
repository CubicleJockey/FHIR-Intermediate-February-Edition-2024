using System.Linq;
using System.Text;
using fi_u2_lib;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class CompareDemographics : BasePatientSearch
    {
        /* Documentation: https://www.hl7.org/fhir/patient.html */
        public string GetDemographicComparison(string serverEndPoint, string identifierSystem, string identifierValue,
            string family, string given, string gender, string birthDate)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if(patient == default) { return PATIENTNOTFOUND; }

            var demographics = new StringBuilder();

            var name = patient.Name.First();
            var fhirFamily = name.Family;
            var givenParts = name.Given.ToArray();

            var fhirGiven = string.Join(" ", givenParts);
            
            var fhirGender = patient.Gender.ToString()?.ToUpper();
            var fhirBirthDate = patient.BirthDate;

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
