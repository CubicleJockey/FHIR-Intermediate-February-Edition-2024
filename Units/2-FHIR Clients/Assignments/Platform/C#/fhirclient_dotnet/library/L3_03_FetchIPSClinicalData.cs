using fi_u2_lib;

namespace fhirclient_dotnet
{
    public class FetchIPS : BasePatientSearch
    {
        /*
         * MedicationStatement IPS Documentation: https://hl7.org/fhir/uv/ips/StructureDefinition-MedicationStatement-uv-ips.html
         * 
         * Immunization IPS Documentation: https://hl7.org/fhir/uv/ips/StructureDefinition-Immunization-uv-ips.html
         */

        public string GetIPSMedications(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }
            
            var aux = "";
            return aux;
        }

        public string GetIPSImmunizations(string serverEndPoint, string identifierSystem, string identifierValue)
        {
            var patient = SearchPatient(serverEndPoint, identifierSystem, identifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }

            var aux = "";
            return aux;
        }
    }
}
