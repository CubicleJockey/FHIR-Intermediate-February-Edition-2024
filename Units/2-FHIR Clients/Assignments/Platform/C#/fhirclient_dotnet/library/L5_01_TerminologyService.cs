using System;
using System.Linq;
using System.Text;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class TerminologyService
    {
        //Example: https://r4.ontoserver.csiro.au/fhir/ValueSet/$expand?filter=Drug-induced diabetes&url=http://snomed.info/sct?fhir_vs=isa/73211009
        public string ExpandValueSetForCombo(string endpoint, string url, string filter)
        {
            using var fhirClient = new FhirClient(endpoint);

            var parameters = new Parameters();
            parameters.Add("url", new FhirUri(url));
            parameters.Add("filter", new FhirString(filter));

            const string operationName = "expand"; //$expand

            const string NOTFOUND = "Error:ValueSet_Filter_Not_Found";
            
            var valueSets = fhirClient.TypeOperation<ValueSet>(operationName, parameters) as ValueSet;
            if (valueSets == default) { return NOTFOUND; }
            
            var response = new StringBuilder();

            var contains = 
                valueSets.Expansion
                         .Contains
                         .Where(contain => contain.Display.Contains(filter))
                         .ToArray();

            if (contains.Length == 0) { return NOTFOUND; }
            
            foreach (var contain in contains)
            {
                response.Append($"{contain.Code}|{contain.Display}\n");
            }
            
            return response.ToString();
        }
    }
}