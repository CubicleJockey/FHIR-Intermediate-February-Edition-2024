package com.fhirintcourse.u2;

public class Config
 {
    
    
         public String ServerEndpoint()
         {
          return  "http://wildfhir4.aegis.net/fhir4-0-1";
         }
            
        public String PatientIdentifierSystem()
        {
          return "http://fhirintermediate.org/patient_id";
        }
            
        public String TerminologyServerEndpoint()
        {
            return "https://snowstorm.ihtsdotools.org/fhir";
        }    
        public String AssignmentSubmissionFHIRServer()
        {return
              "http://fhir.hl7fundamentals.org/r4";
            }

        public String StudentId()
        {
             return "kaminker.diego@gmail.com";
            }
        public String StudentName()
        {return 
            "Diego Kaminker";    
        }
    
}
