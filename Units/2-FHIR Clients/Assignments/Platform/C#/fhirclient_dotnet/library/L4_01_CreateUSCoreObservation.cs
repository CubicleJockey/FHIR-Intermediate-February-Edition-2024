using System.Collections.Generic;
using fi_u2_lib;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Newtonsoft.Json;

namespace fhirclient_dotnet
{
    public class CreateUSCoreObs : BasePatientSearch
    {
        /*
         * Documentation:
         *  1. https://www.hl7.org/fhir/observation.html
         *  2. http://hl7.org/fhir/observation-definitions.html#Observation.interpretation
         *
         * Forum Notes:
         *   Create an Observation with EITHER numeric result value OR coded result value,
         *   and the ResultType provides the information regarding which one to create ("Coded" or "numeric").
         * 
         */
        public string CreateUSCoreR4LabObservation
        (
            string serverEndpoint,
            string patientIdentifierSystem,
            string patientIdentifierValue,
            string observationStatusCode,
            string observationDateTime,
            string observationLOINCCode,
            string observationLOINCDisplay,
            string resultType,
            string numericResultValue,
            string numericResultUCUMUnit,
            string codedResultSNOMEDCode,
            string codedResultSNOMEDDisplay
        )
        {
            var patient = SearchPatient(serverEndpoint, patientIdentifierSystem, patientIdentifierValue);
            if (patient == default) { return PATIENTNOTFOUND; }

            var (datatype, narrative) = 
                CreateValueAndNarrative(resultType, numericResultValue, numericResultUCUMUnit, codedResultSNOMEDCode, codedResultSNOMEDDisplay);
            
            var observation = new Observation
            {
                Subject = GetResourceReference(patient),
                Status = GetObservationStatus(observationStatusCode),
                Effective = new FhirDateTime(observationDateTime),
                Code = new CodeableConcept("http://loinc.org", observationLOINCCode, observationLOINCDisplay),
                Value = datatype,
                Performer = new List<ResourceReference>
                {
                    new ResourceReference { Display = "Doctor Man" }
                },
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = narrative
                }
            };
            
            //using var fhirClient = new FhirClient(serverEndpoint);
            //var createdObservation = fhirClient.Create(observation);

            var serializer = new FhirJsonSerializer();
            var json = serializer.SerializeToString(observation);
            return json;
        }

        #region Helper Methods

        private static ObservationStatus GetObservationStatus(string status)
        {
            var convertedStatus = status.ToLower() switch
            {
                "amended" => ObservationStatus.Amended,
                "cancelled" => ObservationStatus.Cancelled,
                "corrected" => ObservationStatus.Corrected,
                "enteredinerror" => ObservationStatus.EnteredInError,
                "final" => ObservationStatus.Final,
                "preliminary" => ObservationStatus.Preliminary,
                "registered" => ObservationStatus.Registered,
                _ => ObservationStatus.Unknown
            };

            return convertedStatus;
        }

        //Coded Values: "http://snomed.info/sct",
        //
        private static (DataType, string) CreateValueAndNarrative(string resultType, string numericValue, string numericUnit, string code, string display)
        {
            string narrative;
            DataType dataType;
            switch (resultType.ToLower())
            {
                case "numeric":
                    dataType = new Quantity(decimal.Parse(numericValue), numericUnit);
                    narrative = $"<div xmlns=\"http://www.w3.org/1999/xhtml\">{numericValue} {numericUnit}</div>";
                    break;
                case "coded":
                    dataType = new CodeableConcept("http://snomed.info/sct", code, display);
                    narrative = $"<div xmlns=\"http://www.w3.org/1999/xhtml\">{code}:{display}</div>";
                    break;
                default:
                    dataType = default;
                    narrative = default;
                    break;
            }

            return (dataType, narrative);
        }

        private static ResourceReference GetResourceReference(Resource resource)
        {
            var reference = new ResourceReference
            {
                Reference = $"{resource.TypeName}/{resource.Id}"
            };

            return reference;
        }
        
        #endregion Helper Methods
    }
}
