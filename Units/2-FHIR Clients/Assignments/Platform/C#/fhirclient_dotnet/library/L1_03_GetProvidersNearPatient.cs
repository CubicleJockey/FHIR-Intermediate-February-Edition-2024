using Hl7.Fhir.Model;  
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class GetProvidersNearPatient
    {
        public string GetProvidersNearCity
        (string ServerEndPoint,
         string IdentifierSystem,
         string IdentifierValue
         )
         {

             var aux="This is Nothing";
             return aux;

         }

         private Patient FHIR_SearchByIdentifier(string ServerEndPoint, string IdentifierSystem, string IdentifierValue)
        {
            var o = new Patient();
            var client = new FhirClient(ServerEndPoint);
            var bu = client.Search<Patient>(new[]
                {"identifier="  +IdentifierSystem+"|"+IdentifierValue});
            if (bu.Entry.Count > 0)
            {
                o = (Patient)bu.Entry[0].Resource;
            }
            else
            { o = null; }
            return o;
        }

    }
}
