using System;
using Xunit;
using fhirclient_dotnet;
namespace fhirclient_dotnet_tests
{
    public class L03_1_GetClinicalDataImmunization_Tests
    {
        //L03_1_T01: Patient Does Not Exist
        //L03_1_T02: Patient Exists, but has no immunization data
        //L03_1_T03: Patient Exists, has some non-compliant imm resource
        //L03_1_T04: Patient Exists, with one imm resource
        //L03_1_T05: Patient Exists, with several imm resources
      
        [Fact]
        public string L03_1_T01_GetImmunization_NonExistingPatient()

        {
           MyConfiguration c=new MyConfiguration();
            var server=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
               var IdentifierValue="L03_1_T01";
            var ExpImmunizations="Error:Patient_Not_Found";
            var fsh=new FetchImmunization();
            var rm=fsh.GetImmunizations(server,IdentifierSystem,IdentifierValue);
            Assert.True(ExpImmunizations==rm,ExpImmunizations+"!="+rm);
            return rm;
        }
        [Fact]
         public  string L03_1_T02_GetImmunizationsPatientWithNoImmunization()

        {
            MyConfiguration c=new MyConfiguration();
            var server=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
              var IdentifierValue="L03_1_T02";
            var ExpImmunization="Error:No_Immunizations";
            var fsh=new FetchImmunization();
            var rm=fsh.GetImmunizations(server,IdentifierSystem,IdentifierValue);
            Assert.True(ExpImmunization==rm,ExpImmunization+"!="+rm);
           return rm;  
        }

     
 [Fact]
         public  string L03_1_T03_GetImmunizationsPatientWithOneImmunization()

        {
            MyConfiguration c=new MyConfiguration();
            var server=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
              var IdentifierValue="L03_1_T03";
            var ExpImmunization="Completed|158:influenza, injectable, quadrivalent|2020-01-08\n";
            var fsh=new FetchImmunization();
            var rm=fsh.GetImmunizations(server,IdentifierSystem,IdentifierValue);
            Assert.True(ExpImmunization==rm,ExpImmunization+"!="+rm);
            return rm;
        }
[Fact]
         public  string L03_1_T04_GetImmunizationsPatientWithSeveralImmunizations()

        {
            MyConfiguration c=new MyConfiguration();
            var server=c.ServerEndpoint;
            var IdentifierSystem=c.PatientIdentifierSystem;
              var IdentifierValue="L03_1_T04";
            var ExpImmunization="Completed|207:COVID-19, mRNA, LNP-S, PF, 100 mcg/0.5 mL dose|2020-01-10\n";
            ExpImmunization+="Completed|173:cholera, BivWC|2019-10-20\n";
            var fsh=new FetchImmunization();
            var rm=fsh.GetImmunizations(server,IdentifierSystem,IdentifierValue);
            Assert.True(ExpImmunization==rm,ExpImmunization+"!="+rm);
            return rm;
        }

 
    }

}
