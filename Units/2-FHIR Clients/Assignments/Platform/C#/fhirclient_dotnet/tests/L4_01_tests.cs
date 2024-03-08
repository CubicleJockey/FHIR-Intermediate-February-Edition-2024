using Xunit;
using fhirclient_dotnet;
using Hl7.Fhir.Model; 
using Hl7.Fhir.Rest; 

namespace fhirclient_dotnet_tests
{
    public class L04_1_CreateUSCoreLabResult_Tests
    {
        //L04_1_T01: Patient Does Not Exist
        //L04_2_T02: Patient Exists, Create Numerical Observation
        //L03_3_T03: Patient Exists, Create Categorical Observation
  
        [Fact]
        public string L04_1_T01_CreateNumerical_NonExistingPatient()

        {
               var c=new MyConfiguration();
            var server=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
           var IdentifierValue="L04_1_T01";
            var ExpObservations="Error:Patient_Not_Found";
            var ObservationStatusCode="";
            var ObservationDateTime="";
            var ObservationLOINCCode="";
            var ObservationLOINCDisplay="";
            var ResultType="";
            var NumericResultValue="";
            var NumericResultUCUMUnit="";
            var CodedResultSNOMEDCode="";
            var CodedResultSNOMEDDisplay="";
      
            var fsh=new CreateUSCoreObs();
            var rm=fsh.CreateUSCoreR4LabObservation(server,IdentifierSystem,IdentifierValue
            ,ObservationStatusCode,ObservationDateTime,ObservationLOINCCode,ObservationLOINCDisplay,ResultType,
            NumericResultValue,NumericResultUCUMUnit,CodedResultSNOMEDCode,CodedResultSNOMEDDisplay);
            Assert.True(ExpObservations==rm,ExpObservations+"!="+rm);
            return rm;
        }
      
  [Fact]
         public string L04_1_T02_CreateCodedObservation()

        {

               var c=new MyConfiguration();
            var server=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
           var IdentifierValue="L04_1_T02";
            var ExpObservations="Error:Patient_Not_Found";
            var ObservationStatusCode="final";
            var ObservationDateTime="2020-10-11T20:30:00Z";
            var ObservationLOINCCode="5778-6";
            var ObservationLOINCDisplay="Color of Urine";
            var ResultType="Coded";
            var NumericResultValue="";
            var NumericResultUCUMUnit="";
            var CodedResultSNOMEDCode="371244009";
            var CodedResultSNOMEDDisplay="Yellow";
      
            var fsh=new CreateUSCoreObs();
            var rm=fsh.CreateUSCoreR4LabObservation(server,IdentifierSystem,IdentifierValue
            ,ObservationStatusCode,ObservationDateTime,ObservationLOINCCode,ObservationLOINCDisplay,ResultType,
            NumericResultValue,NumericResultUCUMUnit,CodedResultSNOMEDCode,CodedResultSNOMEDDisplay);
           
            if (rm!="Error:Patient_Not_Found")
            {
            ExpObservations=ValidateObservationUSCORE(rm,server);
            
                if (ExpObservations=="")
                {
                    ExpObservations=VerifyObservationContents(rm,server,IdentifierSystem,IdentifierValue
                    ,ObservationStatusCode,ObservationDateTime,ObservationLOINCCode,ObservationLOINCDisplay,ResultType,
                    NumericResultValue,NumericResultUCUMUnit,CodedResultSNOMEDCode,CodedResultSNOMEDDisplay);
                }
            }
            Assert.True(ExpObservations=="",ExpObservations);
             return ExpObservations;
        }


 [Fact]
         public string L04_1_T03_CreateNumericalObservation()

        {
              var c=new MyConfiguration();
            var server=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
            var IdentifierValue="L04_1_T02";
            var ExpObservations="Error:Patient_Not_Found";
            var ObservationStatusCode="final";
            var ObservationDateTime="2020-10-11T20:30:00Z";
            var ObservationLOINCCode="1975-2";
            var ObservationLOINCDisplay="Bilirubin, serum";
            var ResultType="numeric";
            var NumericResultValue="8.6";
            var NumericResultUCUMUnit="mg/dl";
            var CodedResultSNOMEDCode="";
            var CodedResultSNOMEDDisplay="";
      
            var fsh=new CreateUSCoreObs();
            var rm=fsh.CreateUSCoreR4LabObservation(server,IdentifierSystem,IdentifierValue
            ,ObservationStatusCode,ObservationDateTime,ObservationLOINCCode,ObservationLOINCDisplay,ResultType,
            NumericResultValue,NumericResultUCUMUnit,CodedResultSNOMEDCode,CodedResultSNOMEDDisplay);
            ExpObservations=ValidateObservationUSCORE(rm,server);
            
            if (ExpObservations=="")
            {
                ExpObservations=VerifyObservationContents(rm,server,IdentifierSystem,IdentifierValue
                ,ObservationStatusCode,ObservationDateTime,ObservationLOINCCode,ObservationLOINCDisplay,ResultType,
                NumericResultValue,NumericResultUCUMUnit,CodedResultSNOMEDCode,CodedResultSNOMEDDisplay);
            }
            Assert.True(ExpObservations=="",ExpObservations);
            return ExpObservations;
           
        
        }
      
    public string VerifyObservationContents(string JsonObservation,
            string server,string IdentifierSystem,string IdentifierValue
            ,string ObservationStatusCode,string ObservationDateTime,string ObservationLOINCCode,string ObservationLOINCDisplay,string ResultType,
            string NumericResultValue,string NumericResultUCUMUnit,string CodedResultSNOMEDCode,string CodedResultSNOMEDDisplay)

    {
            var aux="";
              var  o=new  Observation() ;
             var parser = new Hl7.Fhir.Serialization.FhirJsonParser();
          
            try
            {
                o = parser.Parse<Observation>(JsonObservation);
                if(o.Status.ToString().ToUpper()!=ObservationStatusCode.ToUpper()){aux+="Status Code Differs";}
                if(o.Code.Coding[0].Code!=ObservationLOINCCode){aux+="Code Differs";}
                if(o.Effective.ToString()!=ObservationDateTime){aux+="Date Differs";}
                if(ResultType=="numeric")
                {
                    var q= (Quantity) o.Value;
                    if (q.Unit!=NumericResultUCUMUnit){aux+="Numeric Result Unit differs";}
                    if (q.Value.ToString()!=NumericResultValue){aux+="Numeric Result Value differs";}
                 }
                else
                {
                    var c=(CodeableConcept) o.Value;
                    if (c.Coding[0].Code!=CodedResultSNOMEDCode){aux+="Coded Result Code differs";}
                }

            }
            catch
            {
                aux="Error:Invalid_Observation_Resource";
            }
           
            return aux;
    }
    
    public string ValidateObservationUSCORE(string JsonObservation,string server)
    {
             var aux="";
             var  o=new  Observation() ;
             var parser = new Hl7.Fhir.Serialization.FhirJsonParser();
          
            try
            {
                o = parser.Parse<Observation>(JsonObservation);
            }
            catch
            {
                aux="Error:Invalid_Observation_Resource";
            }
            if (aux=="")
            {
            var client = new FhirClient(server); 
            var profile=  new FhirUri("http://hl7.org/fhir/us/core/StructureDefinition/us-core-observation-lab");
           
            var inParams = new Parameters();
            inParams.Add("resource", o);
            var bu = client.ValidateResource(o); 
            if (bu.Issue[0].Details.Text!="Validation successful, no issues found")
            {
                aux="Error:"+bu.Issue[0].Details.Text;
            }
            }
            return aux;

    }


}
}