using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class FetchEthnicity
    {
        /* Documentation: https://www.hl7.org/fhir/structuredefinition.html */
        public string GetEthnicity(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return "Error:Patient_Not_Found"; }

            var (extensionFound, ethnicityExtension) = ExtensionContainsOmbCategory(patient.Extension);

            if (patient.Extension.Count == 0 || !extensionFound)
            {
                return "Error:No_us-core-ethnicity_Extension";
            }

            var isConformant = CheckEthnicityExtensionsConformance(ethnicityExtension);
            if (!isConformant) { return "Error:Non_Conformant_us-core-ethnicity_Extension"; }
            
            
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

        private static (bool, Extension) ExtensionContainsOmbCategory(IEnumerable<Extension> extensions)
        {
            var ethnicityExtension = extensions.FirstOrDefault(extension => extension.Url == "ombCategory");
            var found = ethnicityExtension != default;
            return (found, ethnicityExtension);
        }

        private static bool CheckEthnicityExtensionsConformance(Extension extension)
        {
            //TODO: review documentation and complete logic.

            return false;
        }
    }
}
