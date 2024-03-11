using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Collections.Generic;
using System.Linq;
using fi_u2_lib;

namespace fhirclient_dotnet
{
    public class FetchDemographics : BasePatientSearch
    {
        /* Documentation: https://www.hl7.org/fhir/patient.html */
        public string GetPatientPhoneAndEmail(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if(patient == default) { return PATIENTNOTFOUND; }

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
