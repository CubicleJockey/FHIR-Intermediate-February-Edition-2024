using Xunit;
using fhirclient_dotnet;
using Hl7.Fhir.Model; 
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet_tests
{

    public class L04_2_CreateUSCoreImmunization_Tests
    {
        //L04_2_T01: Patient Does Not Exist
        //L04_2_T02: Patient Exists, Was Immunized
        //L04_2_T03: Patient Exists, Was Not Immunized

        [Fact]
        public string L04_2_T01_NonExistingPatient()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L04_2_T01";
            var ExpImmunization = "Error:Patient_Not_Found";
            var ImmunizationStatusCode = "";
            var ImmunizationDateTime = "";
            var ProductCVXCode = "";
            var ProductCVXDisplay = "";
            var ReasonCode = "";
            var fsh = new CreateUSCoreImm();
            var rm = fsh.CreateUSCoreR4Immunization(
                server,
                IdentifierSystem,
                IdentifierValue,
                ImmunizationStatusCode,
                ImmunizationDateTime,
                ProductCVXCode,
                ProductCVXDisplay,
                ReasonCode);

            Assert.True(ExpImmunization == rm, ExpImmunization + "!=" + rm);
            return rm;
        }

        [Fact]
        public string L04_2_T02_ImmunizationCompleted()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L04_2_T02";
            var ExpImmunization = "";
            var ImmunizationStatusCode = "completed";
            var ImmunizationDateTime = "2021-10-25";
            var ProductCVXCode = "173";
            var ProductCVXDisplay = "cholera, BivWC";
            var ReasonCode = "";
            var fsh = new CreateUSCoreImm();
            var rm = fsh.CreateUSCoreR4Immunization(
                server,
                IdentifierSystem,
                IdentifierValue,
                ImmunizationStatusCode,
                ImmunizationDateTime,
                ProductCVXCode,
                ProductCVXDisplay,
                ReasonCode);

            ExpImmunization = ValidateImmunizationUSCORE(rm, server);

            if (ExpImmunization == "")
            {
                ExpImmunization = VerifyImmunizationContents(rm, server,
                    IdentifierSystem,
                    IdentifierValue,
                    ImmunizationStatusCode,
                    ImmunizationDateTime,
                    ProductCVXCode,
                    ProductCVXDisplay,
                    ReasonCode);

            }

            Assert.True(ExpImmunization == "", ExpImmunization);
            if (ExpImmunization == "")
            {
                ExpImmunization = "-";
            }

            return ExpImmunization;
        }

        [Fact]
        public string L04_2_T03_ImmunizationNotDone()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L04_2_T02";
            var ImmunizationStatusCode = "not-done";
            var ImmunizationDateTime = "2021-10-30T10:30:00Z";
            var ProductCVXCode = "173";
            var ProductCVXDisplay = "cholera, BivWC";
            var ReasonCode = "IMMUNE";
            var fsh = new CreateUSCoreImm();
            var rm = fsh.CreateUSCoreR4Immunization(
                server,
                IdentifierSystem,
                IdentifierValue,
                ImmunizationStatusCode,
                ImmunizationDateTime,
                ProductCVXCode,
                ProductCVXDisplay,
                ReasonCode);

            var ExpImmunization = ValidateImmunizationUSCORE(rm, server);

            if (ExpImmunization == "")
            {
                ExpImmunization = VerifyImmunizationContents(rm, server,
                    IdentifierSystem,
                    IdentifierValue,
                    ImmunizationStatusCode,
                    ImmunizationDateTime,
                    ProductCVXCode,
                    ProductCVXDisplay,
                    ReasonCode);

            }

            Assert.True(ExpImmunization == "", ExpImmunization);
            if (ExpImmunization == "")
            {
                ExpImmunization = "-";
            }

            return ExpImmunization;
        }


        public string ValidateImmunizationUSCORE(string JsonImmunization, string server)
        {
            var aux = "";
            var o = new Immunization();
            var parser = new Hl7.Fhir.Serialization.FhirJsonParser();

            try
            {
                o = parser.Parse<Immunization>(JsonImmunization);
            }
            catch
            {
                aux = "Error:Invalid_Immunization_Resource";
            }

            if (aux == "")
            {
                var client = new FhirClient(server);
                var profile = new FhirUri("http://hl7.org/fhir/us/core/StructureDefinition/us-core-immunization");

                var inParams = new Parameters();
                inParams.Add("resource", o);
                var bu = client.ValidateResource(o);

                //COMMENT: Rik Smithies 
                //         The way that OperationOutcome is used to report the error differs from server to server.So you can update the unit test according to how your server does it.
                //if (bu.Issue[0].Details.Text != "Validation successful, no issues found")
                //if (bu.Issue[0].Details.Text != "Validation successful, no issues found")
                if (bu.Issue[0].Diagnostics != "No issues detected during validation")
                {
                    //aux = "Error:" + bu.Issue[0].Details.Text;
                    aux = "Error:" + bu.Issue[0].Diagnostics;
                }
            }

            return aux;

        }


        public string VerifyImmunizationContents(string JsonImmunization,
            string server,
            string IdentifierSystem,
            string IdentifierValue,
            string ImmunizationStatusCode,
            string ImmunizationDateTime,
            string ProductCVXCode,
            string ProductCVXDisplay,
            string ReasonCode
        )
        {
            var aux = "";
            var o = new Immunization();

            var parser = new Hl7.Fhir.Serialization.FhirJsonParser();

            try
            {
                o = parser.Parse<Immunization>(JsonImmunization);
                //if(o.Status.ToString().ToUpper()!=ImmunizationStatusCode.ToUpper()){aux+="Status Code Differs :"+ImmunizationStatusCode.ToUpper()+ "!="+o.Status.ToString().ToUpper() ;}
                //if(o.OcurrenceDateTime.ToString()!=ImmunizationDateTime){aux+="Date Differs";}

                var c = o.VaccineCode;
                if (c.Coding[0].Code != ProductCVXCode)
                {
                    aux += "Vaccine Code code differs";
                }

                if (c.Coding[0].Display != ProductCVXDisplay)
                {
                    aux += "Vaccine Code Display differs";
                }

                if (ImmunizationStatusCode == "not-done")
                {
                    var cr = o.ReasonCode[0];
                    if (cr.Coding[0].Code != ReasonCode)
                    {
                        aux += "Coded Reason code differs";
                    }
                }

            }
            catch
            {
                aux = "Error:Invalid_Immunization_Resource";
            }

            return aux;
        }
    }
}