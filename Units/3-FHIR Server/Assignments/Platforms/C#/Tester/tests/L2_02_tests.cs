using System;
using Xunit;
using fhirserver_dotnet_library;
namespace fhirserver_dotnet_tests
{

    public class L02_2_Tests
    {
        /*
        Tests for U3-L02_2:Practitioner - Direct Get (GetById)
        U3-L02_2_T01:Direct Get to an existing practitioner
        U3-L02_2_T02:Direct Get to an non-existing practitioner
        U3-L02_2_T03:Direct Get to an existing person without NPI
        */
        [Fact]
        public void L02_2_T01_Practitioner_Direct_Get_Exists()

        {
            var result = L02_2_testrunner.L02_2_T01();
            var expected = "McEnroe John Patrick";
            Assert.Equal(expected, result);

        }
        [Fact]
        public void L02_2_T02_Practitioner_Direct_Get_Not_exists()

        {
            var c=new MyConfiguration();
            var result = L02_2_testrunner.L02_2_T02();
            var expected = c.MSG_PractitionerNotFound;
            Assert.Contains(expected, result);

        }
        [Fact]
        public void L02_2_T03_Practitioner_Direct_Get_Not_A_Practitioner()

        {
            var c=new MyConfiguration();
            var result = L02_2_testrunner.L02_2_T03();
            var expected = c.MSG_PersonNotAPractitioner;
            Assert.Contains(expected, result);

        }
    }
}