using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Collections.Generic;
using System.Linq;

namespace fhirclient_dotnet
{
    public class FetchDemographics
    {
        public string GetPatientPhoneAndEmail(string serverEndPoint, string identifierSystem, string identifierValue)
         {
            var patient = new Patient();
            
            using var client = new FhirClient(serverEndPoint);
            var patientSearchBundle = client.Search<Patient>(new[] { $"identifier={identifierSystem}|{identifierValue}" });
            if (patientSearchBundle.Entry.Count > 0)
            {
                patient = (Patient)patientSearchBundle.Entry[0].Resource;
            }
            else { return "Error:Patient_Not_Found"; }

            var phones = new List<string>();
            var emails = new List<string>();
            foreach (var communication in patient.Telecom)
            {
                if (communication.System == ContactPoint.ContactPointSystem.Phone)
                {
                    phones.Add($"{communication.Value}({communication.Use})");
                    continue;
                }

                if (communication.System == ContactPoint.ContactPointSystem.Email)
                {
                    emails.Add($"{communication.Value}({communication.Use})");
                }
            }
            

            var phonesText = phones.Any() ? string.Join(",", phones) : "-";
            var emailsText = emails.Any() ? string.Join(",", emails) : "-";
            
            var aux = $"Emails:{emailsText}\nPhones:{phonesText}\n";
            return aux.ToString();
            
         }
    
    }
}
