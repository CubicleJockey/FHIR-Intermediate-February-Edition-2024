Used Visual Studio 2022, please open with Solution File: FHIRClientAssignment.sln

================================
    NOTES ABOUT CODE CHANGES
================================

Confirmed with Rik Smithies that these changes could be made.


#1
Adjustmnet: L03_3_T04_GetMedicationsPatientWithIPSNoMeds()
Description: In this test the test is expecting Active but server is returning Unknown for status of No Medications.
Forum: https://courses.hl7fundamentals.org/campus/mod/forum/discuss.php?d=7238

File(s):    L3_03.tests.cs
Method(s):  L03_3_T04_GetMedicationsPatientWithIPSNoMeds()
            var ExpMedication was updated to Unknown instead of Active

IMPORTANT: The auto-grader cannot be fixed by me so in the JSON manually change Unknown back to Active for passing of that single test.


#2

Adjustment:  ValidateObservationUSCORE(...), ValidateImmunizationUSCORE(...)
Description: The server was returning Validation no issues text differently and in a different property.
Forum: https://courses.hl7fundamentals.org/campus/mod/forum/discuss.php?d=7241

File(s):    L4_01_tests.cs, L4_01_tests.cs SubmissionCreator.cs
Method(s):  ValidateObservationUSCORE, ValidateImmunizationUSCORE


This below change was made in both methods in ALL three files, which include the SubmissionCreator.cs

//COMMENT: Rik Smithies 
//         The way that OperationOutcome is used to report the error differs from server to server.So you can update the unit test according to how your server does it.
//if (bu.Issue[0].Details.Text != "Validation successful, no issues found")
if (bu.Issue[0].Diagnostics != "No issues detected during validation")
{
    //aux = "Error:" + bu.Issue[0].Details.Text;
    aux = "Error:" + bu.Issue[0].Diagnostics;
}
