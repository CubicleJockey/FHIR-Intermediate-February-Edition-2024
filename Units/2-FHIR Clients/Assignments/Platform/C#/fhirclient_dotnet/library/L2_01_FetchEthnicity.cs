using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi_u2_lib;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class FetchEthnicity : BasePatientSearch
    {
        /* Documentation: https://www.hl7.org/fhir/structuredefinition.html */
        public string GetEthnicity(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return "Error:Patient_Not_Found"; }

            var (extensionFound, ethnicityExtension) = ExtensionContainsUsCoreEthnicity(patient.Extension);

            if (patient.Extension.Count == 0 || !extensionFound)
            {
                return "Error:No_us-core-ethnicity_Extension";
            }

            var (isConformant, code, text) = CheckConformanceAndRetrieveCodeAndText(ethnicityExtension);
            if (!isConformant) { return "Error:Non_Conformant_us-core-ethnicity_Extension"; }

            var details = GetEthnicityDetails(ethnicityExtension).ToArray();
            
            var response = new StringBuilder();
            response.Append($"text|{text}\n");
            response.Append($"code|{code}\n");
            if (details.Any())
            {
                foreach (var detail in details)
                {
                    response.Append($"detail|{detail}\n");
                }
            }
            
            return response.ToString();

        }

        private static (bool found, Extension extension) ExtensionContainsUsCoreEthnicity(IEnumerable<Extension> extensions)
        {
            var invalid = (false, default(Extension));
            
            var ethnicityExtension = extensions.FirstOrDefault(extension => extension.Url == "http://hl7.org/fhir/us/core/StructureDefinition/us-core-ethnicity");
            if (ethnicityExtension == default) { return invalid; }
            
            return (true, ethnicityExtension);
        }

        /// <summary>
        /// Must contain text and OMBCategory
        /// </summary>
        private static (bool isConformant, string code, string text) CheckConformanceAndRetrieveCodeAndText(IExtendable ethnicityExtension)
        {
            var invalid = (false, default(string), default(string));
            
            var ombCategoryExtension = ethnicityExtension.Extension.FirstOrDefault(extension => extension.Url == "ombCategory");
            if (ombCategoryExtension == default) { return invalid; }

            var text = ethnicityExtension.Extension.FirstOrDefault(extension => extension.Url == "text");
            if(text == default) { return invalid; }

            var coding = (Coding)ombCategoryExtension.Value;
            var fhirString = (FhirString)text.Value;

            var code = $"{coding.Code}:{coding.Display}";
            
            return (true, code, fhirString.Value);
        }

        private static IEnumerable<string> GetEthnicityDetails(IExtendable ethnicityExtension)
        {
            var ethnicityDetailCodes = new List<string>();
            foreach (var extension in ethnicityExtension.Extension)
            {
                if (extension.Url != "detailed") { continue; }

                var coding = (Coding)extension.Value;
                ethnicityDetailCodes.Add($"{coding.Code}:{coding.Display}");
            }

            return ethnicityDetailCodes;
        }
    }
}
