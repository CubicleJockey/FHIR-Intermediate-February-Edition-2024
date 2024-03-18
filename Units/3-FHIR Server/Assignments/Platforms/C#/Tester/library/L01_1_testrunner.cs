using System;
namespace fhirserver_dotnet_library
{

    public static class L01_1_testrunner
    {
        public static string L01_1_T01()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var rm = testhelper.CapabilityCheckParameterType(server, "Patient", "email");
            return rm;
        }

        public static string L01_1_T02()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var mail = "john.mcenroe@tennis.com";
            var name = testhelper.PatientSearchByEmail(server, mail);
            return name;
        }

        public static string L01_1_T03()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var mail = "john.mcenroe@tennas.com";
            var name = testhelper.PatientSearchByEmail(server, mail);
            return name;
        }
    }
}