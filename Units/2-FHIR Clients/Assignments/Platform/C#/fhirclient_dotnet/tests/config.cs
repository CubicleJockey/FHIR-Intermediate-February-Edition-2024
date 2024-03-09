namespace fhirclient_dotnet
{
    public class MyConfiguration
    {
        /* Test Servers List: https://confluence.hl7.org/display/FHIR/Public+Test+Servers  */


        /*
         * Local HAPI-FHIR Docker Container
         *
         * Docker Image: https://hub.docker.com/r/hapiproject/hapi
         * 
         * docker pull hapiproject/hapi
         * docker run -p 8080:8080 hapiproject/hapi:latest
         *
         * URLS:
         * Server Configuration: http://localhost:8080/
         * FHIR API Endpoints:   http://localhost:8080/fhir/
         *
         * Setup Data:
         *  Postman Collection: https://tinyurl.com/ficwk2setup
         * 
         */

        //public string ServerEndpoint { get; } = "http://wildfhir4.aegis.net/fhir4-0-1";
        //public string ServerEndpoint { get; } = "http://localhost:8080/fhir/";
        public string ServerEndpoint { get; } = "http://hl7-ips-server.org:8080/fhir";


        public string PatientIdentifierSystem { get; } = "http://fhirintermediate.org/patient_id";
        //public string PatientIdentifierSystem { get; } = "http://localhost:8080/fhir/patient_id";

        public string TerminologyServerEndpoint { get; } = "https://r4.ontoserver.csiro.au/fhir";
        //public string TerminologyServerEndpoint { get; } = "https://demo.kodjin.com/fhir";

        public string AssignmentSubmissionFHIRServer { get; } = "http://fhir.hl7fundamentals.org/r4";

        public string StudentId { get; } = "davis.andre@gmail.com";

        public string StudentName { get; } = "André Davis";

    }
}