using System;
namespace fhirserver_dotnet_library
{

    public static class L01_2_testrunner
    {
        public static string L01_2_T01()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var rm = testhelper.CapabilityCheckParameterType(server, "Patient", "telecom");
            return rm;
        }

        public static string L01_2_T02()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var mail = "email|john.mcenroe@tennis.com";
            var name = testhelper.PatientSearchByTelecom(server, mail);
            return name;
        }

        public static string L01_2_T03()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var mail = "email|john.mcenroe@tennas.com";
            var name = testhelper.PatientSearchByTelecom(server, mail);
            return name;
        }
        public static string L01_2_T04()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var mail = "phone|5555-5555";
            var name = testhelper.PatientSearchByTelecom(server, mail);
            return name;
        }
        public static string L01_2_T05()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var mail = "dorothea.lange@photographer.com";
            var name = testhelper.PatientSearchByTelecom(server, mail);
            return name;
        }
    }
}