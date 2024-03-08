using Hl7.Fhir.Model; 
using Hl7.Fhir.Rest; 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fhirclient_dotnet
{
    public class CreateUSCoreObs
    {
        public string CreateUSCoreR4LabObservation
        (  
        string ServerEndpoint,
        string PatientIdentifierSystem,
        string PatientIdentifierValue,
        string ObservationStatusCode, 
        string ObservationDateTime,
        string ObservationLOINCCode,
        string ObservationLOINCDisplay,
        string ResultType,
        string NumericResultValue,
        string NumericResultUCUMUnit,
        string CodedResultSNOMEDCode,
        string CodedResultSNOMEDDisplay
        )
        {
            String aux="";
            return aux;
        }   
    }
}
