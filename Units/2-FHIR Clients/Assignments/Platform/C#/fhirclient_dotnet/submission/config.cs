namespace fhirclient_dotnet
{
    public  class MyConfiguration
    {

         public string ServerEndpoint{get;}=
            "http://wildfhir4.aegis.net/fhir4-0-0";
        
            
        public string PatientIdentifierSystem{get;}=
            "http://fhirintermediate.org/patient_id";
        
            
        public string TerminologyServerEndpoint{get;}=
            "https://snowstorm.ihtsdotools.org/fhir";
        public string AssignmentSubmissionFHIRServer{get;}=
              "http://fhir.hl7fundamentals.org/r4";

        public string StudentId{get;}=
             "kaminker.diego@gmail.com";
        
        public string StudentName{get;}=
            "Diego Kaminker";    
        
    }

}