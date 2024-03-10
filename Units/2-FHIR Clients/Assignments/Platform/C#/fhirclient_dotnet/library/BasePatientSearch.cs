using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace fi_u2_lib
{
    public abstract class BasePatientSearch
    {
        /* Documentation: https://www.hl7.org/fhir/patient.html */
        protected static Patient SearchPatient(string serverEndPoint, string identifierSystem, string identifierValue)
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
    }
}
