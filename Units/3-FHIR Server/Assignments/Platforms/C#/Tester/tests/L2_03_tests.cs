using Xunit;
using fhirserver_dotnet_library;
namespace fhirserver_dotnet_tests
{

    public class L02_3_Tests
    {

        /*
        Tests for U3-L02_3:Practitioner - Searches by Name & Gender
        U3-L02_3_T01:Get All Practitioners - 5 found w/NPI
        U3-L02_3_T02A:Search All Practitioners by Family Name Lennon- None
        U3-L02_3_T02B:Search All Practitioners by Family Name = McEnroe- 1 match with NPI
        U3-L02_3_T03A:Search All Practitioners by Name = Lennon- None
        U3-L02_3_T03B:Search All Practitioners by Name = John- 3 match with NPI
        U3-L02_3_T04A:Search All male Practitioners : 5
        U3-L02_3_T04B:Search All female Practitioners : 0
        */

        [Fact]
        public void L02_3_T01_Practitioner_Get_All()

        {
            var result = L02_3_testrunner.L02_3_T01();
            var expected = "5:0";
            Assert.Equal(expected, result);

        }
        [Fact]
        public void L02_3_T02A_Practitioner_SearchByFamily_None()

        {
            var result = L02_3_testrunner.L02_3_T02A();
            var expected = "0:0";
            Assert.Equal(expected, result);

        }
        [Fact]
        public void L02_3_T02B_Practitioner_SearchByFamily_One()

        {
            var result = L02_3_testrunner.L02_3_T02B();
            var expected = "1:0";
            Assert.Equal(expected, result);

        }

        [Fact]
        public void L02_3_T03A_Practitioner_SearchByName_None()

        {
            var result = L02_3_testrunner.L02_3_T03A();
            var expected = "0:0";
            Assert.Equal(expected, result);

        }

        [Fact]
        public void L02_3_T03B_Practitioner_SearchByName_Three()

        {
            var result = L02_3_testrunner.L02_3_T03B();
            var expected = "3:0";
            Assert.Equal(expected, result);

        }

        [Fact]
        public void L02_3_T04A_Practitioner_Gender_male()

        {
            var result = L02_3_testrunner.L02_3_T04A();
            var expected = "5:0";
            Assert.Equal(expected, result);

        }


        [Fact]
        public void L02_3_T04A_Practitioner_Gender_female()

        {
            var result = L02_3_testrunner.L02_3_T04B();
            var expected = "0:0";
            Assert.Equal(expected, result);

        }
    }

}
