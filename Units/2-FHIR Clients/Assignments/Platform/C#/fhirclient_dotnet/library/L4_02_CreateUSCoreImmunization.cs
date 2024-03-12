using fi_u2_lib;

namespace fhirclient_dotnet
{
    public class CreateUSCoreImm : BasePatientSearch
    {
        public string CreateUSCoreR4Immunization
        (
            string serverEndpoint,
            string patientIdentifierSystem,
            string patientIdentifierValue,
            string immunizationStatusCode,
            string immunizationDateTime,
            string productCVXCode,
            string productCVXDisplay,
            string reasonCode
        )
        {
            var patient = SearchPatient(serverEndpoint, patientIdentifierSystem, patientIdentifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }
            
            
            var aux = "";
            return aux;
        }
    }
}
