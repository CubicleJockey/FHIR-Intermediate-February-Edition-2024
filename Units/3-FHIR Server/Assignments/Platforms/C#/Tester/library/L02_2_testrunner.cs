namespace fhirserver_dotnet_library
{
    /*
   Tests for 
    U3-L02_2:Practitioner - Direct Get (GetById)
    U3-L02_2_T01:Direct Get to an existing practitioner
    U3-L02_2_T02:Direct Get to an non-existing practitioner
    U3-L02_2_T03:Direct Get to an existing person without NPI
 */
    public static class L02_2_testrunner
    {
        public static string L02_2_T01()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var rm = testhelper.PractitionerGetById(server,"Practitioner/10");
            return rm;
        }

        public static string L02_2_T02()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var rm = testhelper.PractitionerGetById(server,"Practitioner/1008");
            return rm;
        }

        public static string L02_2_T03()

        {
            var c = new MyConfiguration();
            var server = c.ServerEndpoint;
            var rm = testhelper.PractitionerGetById(server,"Practitioner/9");
            return rm;
       }
    }
}