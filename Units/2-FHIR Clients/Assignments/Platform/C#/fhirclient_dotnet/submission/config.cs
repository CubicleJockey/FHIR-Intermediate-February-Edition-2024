namespace fhirclient_dotnet
{
    public class MyConfiguration
    {

        public string ServerEndpoint { get; } = "http://hl7-ips-server.org:8080/fhir";
        public string PatientIdentifierSystem { get; } = "http://fhirintermediate.org/patient_id";
        public string TerminologyServerEndpoint { get; } = "https://r4.ontoserver.csiro.au/fhir";
        public string AssignmentSubmissionFHIRServer { get; } = "http://fhir.hl7fundamentals.org/r4";

        public string StudentId { get; } = "davis.andre@gmail.com";
        public string StudentName { get; } = "André Davis";
    }
}