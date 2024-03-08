using Xunit;
using fhirclient_dotnet;
using Hl7.Fhir.Model; 
using Hl7.Fhir.Rest;

namespace fhirclient_dotnet_tests
{

    public class L00_0_Demo_Tests
    {
        //L00_0_T01: Patient Does Not Exist
        //L00_0_T02: Patient with full name and two addresses

        [Fact]
        public void L00_0_T01_GetNameAndAddresses_NonExistingPatient()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L00_1_T01";
            var NameAndAddresses = "Error:Patient_Not_Found";
            var fsh = new Demo();
            var rm = fsh.GetPatientFullNameAndAddress(server, IdentifierSystem, IdentifierValue);
            Assert.Equal(NameAndAddresses, rm);

        }

        [Fact]
        public void L00_0_T02_GetNameAndAddresses_MultipleAddresses()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L00_1_T02";
            // Spec
            // GetPatientFullNameAndAddress shall return a string with the Full Name of the patient
            // and all the addresses.
            // Format: 
            // Full Name:{family},{given}\n
            // (for each patient's address)
            // [Address:{line} - {city} , {state} , {country} (postalCode) / ]\n 
            // Message if patient not found: Error:Patient_Not_Found
            //
            var ExpectedResult = "FULL NAME:PATIENT L00_1_T02 FAM,PATIENT L00_1_T02 GIV\n";
            ExpectedResult +=
                "ADDRESS:128 THIS PATIENT DRIVE SUITE 318  - ANN ARBOR , MI , US (48103) / 256 THIS PATIENT AVE SUITE 320 - PENT HOUSE  - MONROE , MI , US (48161) / \n";

            var fsh = new Demo();
            var rm = fsh.GetPatientFullNameAndAddress(server, IdentifierSystem, IdentifierValue);
            Assert.Equal(ExpectedResult.ToUpper(), rm.ToUpper());

        }

        [Fact]
        public void L00_0_T03_GetUSCoreR4RaceExtension()
        {


            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L00_1_T03";
            var ExpectedResult = "TEXT|MIXED\nCODE|2028-9:ASIAN\nDETAIL|1586-7:SHOSHONE\nDETAIL|2036-2:FILIPINO\n";
            // Spec
            // GetUSCoreRace shall return a string with the contents of the US-Core race extension
            // Format: 
            // text|{text sub extension}\n (mandatory)
            // code|{coded sub extension}\n (mandatory)
            // detail|{detailed sub extensions}\n (may repeat)
            // Message if patient not found: Error:Patient_Not_Found
            // Message if extension not found: see implementation
            // Message if extension not conformant: see implementation
            //
            var fsh = new Demo();
            var rm = fsh.GetUSCoreRace(server, IdentifierSystem, IdentifierValue);
            Assert.Equal(ExpectedResult.ToUpper(), rm.ToUpper());
        }

        [Fact]

        public void L00_0_T04_GetUSCoreConditions()
        {


            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L00_1_T04";
            // Spec
            // GetConditions shall return a string with the contents of all patient's US CORE Conditions
            // containing mandatory elements, as follows:
            // Format: for each condition:
            // {ClinicalStatus}|{VerificationStatus}|{Category}|{Code.code}:{Code.display}\n
            // Message if patient not found: Error:Patient_Not_Found
            // Message if no conditions found: see implementation
            //
            var ExpectedResult =
                "ACTIVE|CONFIRMED|PROBLEM LIST ITEM|442311008:LIVEBORN BORN IN HOSPITAL\nACTIVE|CONFIRMED|PROBLEM LIST ITEM|69347004:NEONATAL JAUNDICE (DISORDER)\n";
            var fsh = new Demo();
            var rm = fsh.GetConditions(server, IdentifierSystem, IdentifierValue);
            Assert.Equal(ExpectedResult.ToUpper(), rm.ToUpper());
        }

        [Fact]
        public void L00_0_T05_CreateUSCoreAllergyIntolerance()
        {


            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;

            var IdentifierValue = "L00_1_T04";

            var ClinicalStatusCode = "active";
            var VerificationStatusCode = "confirmed";
            var CategoryCode = "medication";
            var CriticalityCode = "high";
            var AllergySnomedCode = "387406002";
            var AllergySnomedDisplay = "Sulfonamide (substance)";
            var ManifestationSnomedCode = "271807003";
            var ManifestationSnomedDisplay = "Skin Rash";
            var ManifestationSeverityCode = "mild";
            // Spec
            // CreateUSCoreAllergyIntolerance shall return a string with a JSON FHIR Resource 
            // conformant with the US Core AllergyIntolerance profile
            // or Message if patient not found: Error:Patient_Not_Found
            //

            var fsh = new Demo();
            var rm =
                fsh.CreateUSCoreAllergyIntolerance(server, IdentifierSystem, IdentifierValue, ClinicalStatusCode,
                    VerificationStatusCode, CategoryCode, CriticalityCode, AllergySnomedCode, AllergySnomedDisplay,
                    ManifestationSnomedCode, ManifestationSnomedDisplay, ManifestationSeverityCode);
            if (rm != "")
            {
                var val = ValidateAllergyIntoleranceUSCORE(rm, server);

                Assert.Equal("OK", val);

            }
            else
            {
                Assert.NotEqual("", rm);
            }
        }

        [Fact]
        public void L00_0_T06_ExpandFilterExistingTerm()
        {

            var c = new MyConfiguration();
            var server = c.TerminologyServerEndpoint;

            // Spec
            // ExpandValueset shall return a string with one record for each concept found in the expansion
            // {code}|{display}
            // or Message if term not found: Error:Term/Concept not found
            //

            var ExpTerms = "64432007|MYOCARDIAL IMAGING\n";
            var url = "http://snomed.info/sct?fhir_vs";
            var filter = "Radioisotope myocardial imaging procedure";
            var fsh = new Demo();
            var rm = fsh.ExpandValueSetForCombo(
                server,
                url,
                filter);
            Assert.True(ExpTerms.ToUpper() == rm.ToUpper(), ExpTerms.ToUpper() + "!=" + rm.ToUpper());


        }

        [Fact]
        public void L00_0_T07_GetIPSLabResults()
        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var IdentifierSystem = c.PatientIdentifierSystem;
            var IdentifierValue = "L03_3_T03";

            var match =
                "882-1:ABO and Rh group [Type] in Blood|2015-10-10T09:15:00+01:00|final|278149003:Blood group A Rh(D) positive\n";
            match += "945-6:C Ab [Presence] in Serum or Plasma|2015-10-10T09:35:00+01:00|final|10828004:Positive\n";
            match += "1018-1:E Ab [Presence] in Serum or Plasma|2015-10-10T09:35:00+01:00|final|10828004:Positive\n";
            match +=
                "1156-9:little c Ab [Presence] in Serum or Plasma|2015-10-10T09:35:00+01:00|final|260385009:Negative\n";
            match += "17856-6:Hemoglobin A1c/Hemoglobin.total in Blood by HPLC|2017-11-10T08:20:00+01:00|final|7.5 %\n";
            match +=
                "42803-7:Bacteria identified in Isolate|2017-12-10T08:20:00+01:00|final|115329001:Methicillin resistant Staphylococcus aureus\n";
            // Spec
            // GetIPSLabResuls shall search an IPS document based on the patient's internal id
            // and then return a string with one record for each IPS lab result Observation
            // with the format
            // {code}:{display}|{effectiveDateTime}|{status}|{result}\n
            // {result} maybe {quantity units} or {code:display} or {string} depending on the result type

            var fsh = new Demo();
            var rm = fsh.GetIPSLabResults(server, IdentifierSystem, IdentifierValue);
            Assert.Equal(match.ToUpper(), rm.ToUpper());
        }

        public string ValidateAllergyIntoleranceUSCORE(string JsonAllergy, string server)
        {
            var aux = "";
            var o = new AllergyIntolerance();

            var parser = new Hl7.Fhir.Serialization.FhirJsonParser();

            try
            {
                o = parser.Parse<AllergyIntolerance>(JsonAllergy);
            }
            catch
            {
                aux = "Error:Invalid_AllergyIntolerance_Resource";
            }

            if (aux == "")
            {
                var client = new FhirClient(server);
                var profile = new FhirUri("http://hl7.org/fhir/us/core/StructureDefinition/us-core-allergyintolerance");

                var inParams = new Parameters();
                inParams.Add("resource", o);
                var bu = client.ValidateResource(o, profile: profile);
                aux = "OK";
                if (bu.Issue[0].Details.Text != "Validation successful, no issues found")
                {
                    aux = "Error:" + bu.Issue[0].Details.Text;
                }
            }

            return aux;

        }

    }

}
