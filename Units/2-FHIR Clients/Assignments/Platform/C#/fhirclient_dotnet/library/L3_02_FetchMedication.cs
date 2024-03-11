using System.Linq;
using System.Text;
using fi_u2_lib;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet
{
    public class FetchMedication : BasePatientSearch
    {
        /*Documentation: https://www.hl7.org/fhir/medicationrequest.html */
        public string GetMedications(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return "Error:Patient_Not_Found"; }

            using var fhirClient = new FhirClient(serverEndPoint);
            var medicationRequestSearch = fhirClient.Search<MedicationRequest>(new[] { $"patient={patient.Id}" });
            if (medicationRequestSearch.Entry.Count == 0)
            {
                return "Error:No_Medications";
            }

            var response = new StringBuilder();
            foreach (var entry in medicationRequestSearch.Entry)
            {
                var medicationRequest = (MedicationRequest)entry.Resource;

                var status = medicationRequest.Status;
                var intent = medicationRequest.Intent;
                var authoredOn = medicationRequest.AuthoredOn;
                var medication = ((CodeableConcept)medicationRequest.Medication).Coding.First();
                var requester = medicationRequest.Requester.Display;

                response.Append($"{status}|{intent}|{authoredOn}|{medication.Code}:{medication.Display}|{requester}\n");
            }

            return response.ToString();
         }        
    }
}
