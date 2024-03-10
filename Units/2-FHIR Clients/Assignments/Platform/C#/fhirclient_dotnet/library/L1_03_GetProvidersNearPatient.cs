using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi_u2_lib;
using Hl7.Fhir.Model;  
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class GetProvidersNearPatient : BasePatientSearch
    {
        public string GetProvidersNearCity(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            
            if (patient == default) { return "Error:Patient_Not_Found"; }
            if (!patient.Address.Any()) { return "Error:Patient_w/o_City"; }

            var patientCity = patient.Address.First().City;
            var providers = SearchProviderByCity(serverEndPoint, patientCity);

            if (providers == default || !providers.Any()) { return "Error:No_Provider_In_Patient_City"; }

            var response = new StringBuilder();
            foreach (var provider in providers)
            {
                var name = provider.Name.First();
                var given = name.Given.ToArray()[0];
                var fullName = $"{name.Family},{given}";
                var addressLine = provider.Address.First().Line.First();
                var phone = provider.Telecom.First().Value;
                var qualification = provider.Qualification.First().Code.Coding.First().Display;
                
                response.Append($"{fullName}|Phone:{phone}|{addressLine}|{qualification}\n");
            }

            return response.ToString();
        }

        /* Documentation: https://www.hl7.org/fhir/practitioner.html */
        private static List<Practitioner> SearchProviderByCity(string serverEndpoint, string city)
        {
            var providers = new List<Practitioner>();
            using var client = new FhirClient(serverEndpoint);
            var providerResults = client.Search<Practitioner>(new [] { $"address-city={city}"});
            if (providerResults.Entry.Count > 0)
            {
                providers.AddRange(providerResults.Entry.Select(provider => (Practitioner)provider.Resource));
            }
            else { providers = default; }
            
            return providers;
        }

    }
}
