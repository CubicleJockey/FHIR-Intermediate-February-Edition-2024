using System;
namespace fhirserver_dotnet_library
{
public static class L03_3_testrunner
    {
        public static string L03_3_T01A()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var ValidationServer=c.ValidationServerEndpoint;
            var rm = testhelper.MedicationRequestGetAndValidate(server, "MedicationRequest/Mi0yOC0yMDIyMTAxMC0xMDQ5NjIz",ValidationServer);
            return rm;
        }

        public static string L03_3_T02A()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var rm = testhelper.MedicationRequestFindRequesterDisplay(server, "MedicationRequest/Mi0yOC0yMDIyMTAxMC0xMDQ5NjIz");

            return rm;
        }
        public static string L03_3_T03A()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var rm = testhelper.MedicationRequestFindWarningSIG(server, "MedicationRequest/Mi0yOC0yMDIyMTAxMC0xMDQ5NjIz");
            return rm;
        }

    }

}