using System;
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

             string aux="This is Nothing";
             return aux;

         }

         private Hl7.Fhir.Model.Patient FHIR_SearchByIdentifier(string ServerEndPoint, string IdentifierSystem, string IdentifierValue)
        {
            Hl7.Fhir.Model.Patient o = new Hl7.Fhir.Model.Patient();
            var client = new Hl7.Fhir.Rest.FhirClient(ServerEndPoint);
            Bundle bu = client.Search<Hl7.Fhir.Model.Patient>(new string[]
                {"identifier="  +IdentifierSystem+"|"+IdentifierValue});
            if (bu.Entry.Count > 0)
            {
                o = (Hl7.Fhir.Model.Patient)bu.Entry[0].Resource;
            }
            else
            { o = null; }
            return o;
        }

    }
}
