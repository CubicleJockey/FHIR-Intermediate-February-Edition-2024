using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System.Collections.Generic;
using fhirclient_dotnet;
namespace fhirclient_dotnet_submission
{
    public class SubmissionCreator
    {
      
        public class TestResult
        {
            public string Code;
            public string Name;
            public string Result;
        }
     
        public Dictionary<string,string> L01_1_Tests()
        {
            var c=new MyConfiguration();
            var ServerAddress=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
             
            var tl=new Dictionary<string, string>();
            var tr=new Dictionary<string, string>();
            tl.Add("L01_1_T01","L01_1_T01");
            tl.Add("L01_1_T02","L01_1_T02");
            tl.Add("L01_1_T03","L01_1_T03");
            tl.Add("L01_1_T04","L01_1_T04");
            tl.Add("L01_1_T05","L01_1_T05");
            tl.Add("L01_1_T06","L01_1_T06");
            foreach(var test in tl)
            {
          
                    var IdentifierValue=test.Value;
                    var L01_1=new FetchDemographics();
                    var result= L01_1.GetPatientPhoneAndEmail(ServerAddress,IdentifierSystem,IdentifierValue);
                    tr.Add(test.Key,result);
            }
            return tr;
        }

    public class Person {
        public String identifier;
        public String family;
        public String given;
        public String gender;
        public String birthDate;

        public Person(String Identifier, String Family, String Given, String Gender, String BirthDate) {
            identifier = Identifier;
            family = Family;
            given = Given;
            gender = Gender;
            birthDate = BirthDate;
        }

    }

    private static Dictionary<String, String> L01_2_Tests() {
        var my_results = new Dictionary<String, String>();

        var my_tests = new Dictionary<String, Person>();
        
        my_tests.Add("L01_2_T01", new Person("L01_2_T01", "", "", "", ""));
        my_tests.Add("L01_2_T02", new Person("L01_2_T02", "Dougras", "Jamieson Harris", "male", "1968-07-23"));
        my_tests.Add("L01_2_T03", new Person("L01_2_T02", "Douglas", "Jamieson", "male", "1968-07-23"));
        my_tests.Add("L01_2_T04", new Person("L01_2_T02", "Douglas", "Jamieson Harris", "male", "1968-07-24"));
        my_tests.Add("L01_2_T05", new Person("L01_2_T02", "Douglas", "Jamieson Harris", "female", "1968-07-23"));
        my_tests.Add("L01_2_T06", new Person("L01_2_T02", "Douglas", "Jamieson Harris", "male", "1968-07-23"));

         foreach(var test in my_tests)
            {
                var aux = "";
           
                var c=new MyConfiguration();
                var ServerAddress=c.ServerEndpoint;
                var IdentifierSystem=c.PatientIdentifierSystem;
                var p=test.Value;
                var IdentifierValue = p.identifier;
                var myFamily = p.family;
                var myGiven = p.given;
                var myGender = p.gender;
                var myBirth = p.birthDate;
                try {
                    var L01_2=new CompareDemographics();
                    aux = L01_2.GetDemographicComparison(ServerAddress, IdentifierSystem, IdentifierValue, myFamily, myGiven,
                    
                    myGender, myBirth);

                } catch (Exception e) {
                    aux = e.Message;

                }
                my_results.Add(test.Key, aux);
            }
        
        return my_results;
    }

        private Dictionary<string,string> L01_3_Tests()
        {
            var c=new MyConfiguration();
            var ServerAddress=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
             
            var tl=new Dictionary<string, string>();
            var tr=new Dictionary<string, string>();
            tl.Add("L01_3_T01","L01_3_T01");
            tl.Add("L01_3_T02","L01_3_T02");
            tl.Add("L01_3_T03","L01_3_T03");
            tl.Add("L01_3_T04","L01_3_T04");
            tl.Add("L01_3_T05","L01_3_T05");
            foreach(var test in tl)
            {
          
                    var IdentifierValue=test.Value;
                    var L01_3=new GetProvidersNearPatient();
                    var result= L01_3.GetProvidersNearCity(ServerAddress,IdentifierSystem,IdentifierValue);
                    tr.Add(test.Key,result);
            }
            return tr;
        }
   private Dictionary<string,string> L03_1_Tests()
        {
            var c=new MyConfiguration();
            var ServerAddress=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
             
            var tl=new Dictionary<string, string>();
            var tr=new Dictionary<string, string>();
            tl.Add("L03_1_T01","L03_1_T01");
            tl.Add("L03_1_T02","L03_1_T02");
            tl.Add("L03_1_T03","L03_1_T03");
            tl.Add("L03_1_T04","L03_1_T04");
            foreach(var test in tl)
            {
          
                    var IdentifierValue=test.Value;
                    var L03_1=new FetchImmunization();
                    var result= L03_1.GetImmunizations(ServerAddress,IdentifierSystem,IdentifierValue);
                    tr.Add(test.Key,result);
            }
            return tr;
        }

        private Dictionary<string,string> L03_2_Tests()
        {
            var c=new MyConfiguration();
            var ServerAddress=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
             
            var tl=new Dictionary<string, string>();
            var tr=new Dictionary<string, string>();
            tl.Add("L03_2_T01","L03_2_T01");
            tl.Add("L03_2_T02","L03_2_T02");
            tl.Add("L03_2_T03","L03_2_T03");
            tl.Add("L03_2_T04","L03_2_T04");
            foreach(var test in tl)
            {
          
                    var IdentifierValue=test.Value;
                    var L03_2=new FetchMedication();
                    var result= L03_2.GetMedications(ServerAddress,IdentifierSystem,IdentifierValue);
                    tr.Add(test.Key,result);
            }
            return tr;
        }
        private Dictionary<string,string> L03_3_Tests()
        {
            var c=new MyConfiguration();
            var ServerAddress=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
             
            var tl=new Dictionary<string, string>();
            var tr=new Dictionary<string, string>();
            tl.Add("L03_3_T01","L03_3_T01");
            tl.Add("L03_3_T02","L03_3_T02");
            tl.Add("L03_3_T03","L03_3_T03");
            tl.Add("L03_3_T04","L03_3_T04");
            tl.Add("L03_3_T05","L03_3_T04");
            tl.Add("L03_3_T06","L03_3_T03");
            
            foreach(var test in tl)
            {
          
                    var IdentifierValue=test.Value;
                    var L03_3=new FetchIPS();
                    var kind="medications";
                    var result="";
                    if (test.Key=="L03_3_T05"){kind="immunizations";}
                    if (test.Key=="L03_3_T06"){kind="immunizations";}
                    if (kind=="immunizations")
                    {
                     result= L03_3.GetIPSImmunizations(ServerAddress,IdentifierSystem,IdentifierValue) ;  
                    }
                    else
                    {
                        result= L03_3.GetIPSMedications(ServerAddress,IdentifierSystem,IdentifierValue);
                    }

                    tr.Add(test.Key,result);
            }
            return tr;
        }

        public  class Immun {
        public String identifierValue;
        public String immunizationStatusCode;
        public String immunizationDateTime;
        public String productCVXCode;
        public String productCVXDisplay;
        public String reasonCode;

        public Immun(String IdentifierValue, String ImmunizationStatusCode, String ImmunizationDateTime,
                String ProductCVXCode, String ProductCVXDisplay, String ReasonCode

        ) {
            identifierValue = IdentifierValue;
            immunizationStatusCode = ImmunizationStatusCode;
            immunizationDateTime = ImmunizationDateTime;
            productCVXCode = ProductCVXCode;
            productCVXDisplay = ProductCVXDisplay;
            reasonCode = ReasonCode;
        }

    }
    public  class LabResult {
        public String identifierValue;
        public String observationStatusCode;
        public String observationDateTime;
        public String observationLOINCCode;
        public String observationLOINCDisplay;
        public String resultType;
        public String numericResultValue;
        public String numericResultUCUMUnit;
        public String codedResultSNOMEDCode;
        public String codedResultSNOMEDDisplay;

        public LabResult(String IdentifierValue, String ObservationStatusCode, String ObservationDateTime,
                String ObservationLOINCCode, String ObservationLOINCDisplay, String ResultType,
                String NumericResultValue, String NumericResultUCUMUnit, String CodedResultSNOMEDCode,
                String CodedResultSNOMEDDisplay) {
            identifierValue = IdentifierValue;
            observationStatusCode = ObservationStatusCode;
            observationDateTime = ObservationDateTime;
            observationLOINCCode = ObservationLOINCCode;
            observationLOINCDisplay = ObservationLOINCDisplay;
            resultType = ResultType;
            numericResultValue = NumericResultValue;
            numericResultUCUMUnit = NumericResultUCUMUnit;
            codedResultSNOMEDCode = CodedResultSNOMEDCode;
            codedResultSNOMEDCode = CodedResultSNOMEDDisplay;
        }

    }

        public static Dictionary<String, String> L04_1_Tests() {
        var my_results = new Dictionary<String, String>();

        var my_tests = new Dictionary<String, LabResult>();
        my_tests.Add("L04_1_T01", new LabResult("L04_1_T01", "", "", "", "", "", "", "", "", ""));
        my_tests.Add("L04_1_T02", new LabResult("L04_1_T02", "final", "2020-10-11T20:30:00Z", "5778-5",
                "Color or Urine", "coded", "", "", "371244009", "Yellow"));
        my_tests.Add("L04_1_T03", new LabResult("L04_1_T02", "final", "2020-10-11T20:30:00Z", "1975-2",
                "Bilirubin, serum", "numeric", "8.6", "mg/dl", "", ""));

        var c=new MyConfiguration();
        var ServerAddress=c.ServerEndpoint;
        var IdentifierSystem=c.PatientIdentifierSystem;
        
            foreach(var test in my_tests)
            {
                var aux="";
                var str = test.Key;
                var l = test.Value;
                var IdentifierValue = l.identifierValue;
                var ObservationStatusCode = l.observationStatusCode;
                var ObservationDateTime = l.observationDateTime;
                var ObservationLOINCCode = l.observationLOINCCode;
                var ObservationLOINCDisplay = l.observationLOINCDisplay;
                var ResultType = l.resultType;
                var NumericResultValue = l.numericResultValue;
                var NumericResultUCUMUnit = l.numericResultUCUMUnit;
                var CodedResultSNOMEDCode = l.codedResultSNOMEDCode;
                var CodedResultSNOMEDDisplay = l.codedResultSNOMEDDisplay;
                var L04_1=new CreateUSCoreObs();
                try {

                    aux = L04_1.CreateUSCoreR4LabObservation(ServerAddress, IdentifierSystem, IdentifierValue,
                    ObservationStatusCode, ObservationDateTime, ObservationLOINCCode, ObservationLOINCDisplay,
                    ResultType, NumericResultValue, NumericResultUCUMUnit, CodedResultSNOMEDCode,
                    CodedResultSNOMEDDisplay);
                    if(aux!="")
                    {
                        if (str!="L04_1_T01")
                        {
                          aux=ValidateObservationUSCORE(aux,ServerAddress);
                        }
                        
                    }
                }
                catch(Exception e){aux=e.Message;}
                
                my_results.Add(test.Key,aux);


        }
        return my_results;
    }

    
        public static Dictionary<String, String> L04_2_Tests() {
        var my_results = new Dictionary<String, String>();

        var my_tests = new Dictionary<String, Immun>();
        my_tests.Add("L04_2_T01", new Immun("L04_2_T01", "", "", "", "", ""));
        my_tests.Add("L04_2_T02", new Immun("L04_2_T02", "completed", "2021-10-25", "173", "", "cholera, BivWC"));
        my_tests.Add("L04_2_T03", new Immun("L04_2_T02", "not-done", "2021-10-30T10:30:00Z", "207",
                "COVID-19, mRNA, LNP-S, PF, 100 mcg/0.5 mL dose", "IMMUNE"));
        var c=new MyConfiguration();
        var ServerAddress=c.ServerEndpoint;
        var IdentifierSystem=c.PatientIdentifierSystem;
        
            foreach(var test in my_tests)
            {
                var aux="";
                var str = test.Key;
                var i = test.Value;
                var IdentifierValue = i.identifierValue;
                var ImmunizationStatusCode = i.immunizationStatusCode;
                var ImmunizationDateTime = i.immunizationDateTime;
                var ProductCVXCode = i.productCVXCode;
                var ProductCVXDisplay = i.productCVXDisplay;
                var ReasonCode = i.reasonCode;
                try {
                     

                    var L04_2=new CreateUSCoreImm();
                 
                    aux = L04_2.CreateUSCoreR4Immunization(ServerAddress, IdentifierSystem, IdentifierValue,
                    ImmunizationStatusCode, ImmunizationDateTime, ProductCVXCode, ProductCVXDisplay, ReasonCode);
                    if(aux!="")
                    {
                        if (str!="L04_2_T01")
                        {
                          aux=ValidateImmunizationUSCORE(aux,ServerAddress);
                        }
                        
                    }
                }
                catch(Exception e){aux=e.Message;}
                
                my_results.Add(test.Key,aux);
            }
            
        return my_results;
    }
        public static string ValidateImmunizationUSCORE(string JsonImmunization,string server)
        {
             var aux="";
             var o=new  Immunization() ;
             var parser = new Hl7.Fhir.Serialization.FhirJsonParser();
          
            try
            {
                o = parser.Parse<Immunization>(JsonImmunization);
            }
            catch
            {
                aux="Error:Invalid_Immunization_Resource";
            }

            if (aux=="")
            {
                var client = new FhirClient(server); 
                if (o.Meta.Profile is null)
                   o.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-immunization" };
                   
                var inParams = new Parameters();
                inParams.Add("resource", o);
                var bu = client.ValidateResource(o); 
                aux="OK";
                if (bu.Issue[0].Details.Text!="Validation successful, no issues found")
                {
                    aux="Error:"+bu.Issue[0].Details.Text;
                }
            }
            return aux;

    }
   public static string ValidateObservationUSCORE(string JsonObservation,string server)
        {
             var aux="";
             var o=new  Observation() ;
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
                if (o.Meta.Profile is null)
                   o.Meta.Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-observation-lab" };
                   
                var inParams = new Parameters();
                inParams.Add("resource", o);
                var bu = client.ValidateResource(o); 
                aux="OK";
                if (bu.Issue[0].Details.Text!="Validation successful, no issues found")
                {
                    aux="Error:"+bu.Issue[0].Details.Text;
                }
            }
            return aux;

    }
    
        private Dictionary<string,string> L05_1_Tests()
        {
            var c=new MyConfiguration();
            var ServerAddress=c.TerminologyServerEndpoint;
            
             
            var tl=new Dictionary<string, string>();
            tl.Add("L05_1_T01", "diaxetes");
            tl.Add("L05_1_T02","Drug-induced diabetes");
            var url="http://snomed.info/sct?fhir_vs=isa/73211009";

            var tr = new Dictionary<string, string>();
            foreach (var test in tl)
            {
                var filter = test.Value;
                var fsh = new TerminologyService();
                var result = fsh.ExpandValueSetForCombo(
                     ServerAddress,
                     url,
                     filter);
                tr.Add(test.Key, result);

            }
            return tr;
         
        }
        
        private Dictionary<string,string> L02_1_Tests()
        {
            var c=new MyConfiguration();
            var ServerAddress=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
             
            var tl=new Dictionary<string, string>();
            var tr=new Dictionary<string, string>();
            tl.Add("L02_1_T01","L02_1_T01");
            tl.Add("L02_1_T02","L02_1_T02");
            tl.Add("L02_1_T03","L02_1_T03");
            tl.Add("L02_1_T04","L02_1_T04");
            tl.Add("L02_1_T05","L02_1_T05");
            
            
            foreach(var test in tl)
            {
          
                    var IdentifierValue=test.Value;
                    var L02_1=new FetchEthnicity();
                    

                    var result= L02_1.GetEthnicity(ServerAddress,IdentifierSystem,IdentifierValue);
                    tr.Add(test.Key,result);
            }
            return tr;
        }    
        public Dictionary<String,String> AddAll(Dictionary<String,String> tAll,Dictionary<String,String> tOne)
        {
            var tl=new Dictionary<string, string>();
            foreach(var test in tAll)
            {
                tl.Add(test.Key,test.Value);

            }
            foreach(var test in tOne)
            {
                Console.WriteLine("adding...");
                Console.WriteLine(test.Key);     
                tl.Add(test.Key,test.Value);

            }
            return tl;  

        }
        public String CreateSubmission()
        {
           
            var c=new MyConfiguration();


            var TestList=new Dictionary<string, string>();
            TestList=AddAll (TestList,L01_1_Tests());
            TestList=AddAll (TestList,L01_2_Tests());
            TestList=AddAll (TestList,L01_3_Tests());
            TestList=AddAll (TestList,L02_1_Tests());
            TestList=AddAll (TestList,L03_1_Tests());
            TestList=AddAll (TestList,L03_2_Tests());
            TestList=AddAll (TestList,L03_3_Tests());
            TestList=AddAll (TestList,L04_1_Tests());
            TestList=AddAll (TestList,L04_2_Tests());
            TestList=AddAll (TestList,L05_1_Tests());
                  
          
            
            var  tr=new  TestReport();
            tr.Result=TestReport.TestReportResult.Pending;
            var datee=DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            datee=datee.Replace(":","");
            tr.Identifier=new Identifier("http://fhirintermediate.org/test_report/id",c.StudentId+"-"+datee);
            tr.Issued=datee;
            tr.Status=TestReport.TestReportStatus.InProgress;
            var r=new ResourceReference();
            r.Identifier=new Identifier("http://fhirintermediate.org/test_script/id","FHIR_INTERMEDIATE_U02-.NET");
            tr.TestScript=r;
            tr.Tester=c.StudentId;

            var pcS=new TestReport.ParticipantComponent();
            pcS.Type=TestReport.TestReportParticipantType.Server;
            pcS.Uri=c.ServerEndpoint;
            pcS.Display="Resource Server";
            tr.Participant.Add(pcS);
            
            var pcTS=new TestReport.ParticipantComponent();
            pcTS.Type=TestReport.TestReportParticipantType.Server;
            pcTS.Uri=c.TerminologyServerEndpoint;
            pcTS.Display="Terminology Server";
            tr.Participant.Add(pcTS);
            
            var pcC=new TestReport.ParticipantComponent();
            pcC.Type=TestReport.TestReportParticipantType.Client;
            pcC.Uri="http://localhost";
            pcC.Display=c.StudentName;
            tr.Participant.Add(pcC);

            foreach(var test in TestList)
            {
                
                var testComponent=new TestReport.TestComponent();
                testComponent.Name=test.Key;
                testComponent.Description=test.Key;
                var ta=new TestReport.TestActionComponent();
                var tac=new TestReport.AssertComponent();
                if (test.Value=="")
                {
                    tac.Result=TestReport.TestReportActionResult.Fail;
                    tac.Message=new Markdown("Not Attempted");
                }
                else
                {
                    tac.Result=TestReport.TestReportActionResult.Pass;
                    tac.Message=new Markdown(test.Value);
                    
                }
                ta.Assert=tac;
                testComponent.Action.Add(ta);
                tr.Test.Add(testComponent);
            }
            
            
            var s = new Hl7.Fhir.Serialization.FhirJsonSerializer();  
            var results = s.SerializeToString(tr);  
            var filename=@"FHIR_INTERMEDIATE_U2_SUBMISSION_" + c.StudentId+"_"+datee+".JSON";
            System.IO.File.WriteAllText(filename, results);
            return filename;


        }
        
      

   }
}