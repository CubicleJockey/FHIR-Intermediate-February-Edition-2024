using Xunit;
using fhirserver_dotnet_library;
namespace fhirserver_dotnet_tests
{

    public class L02_1_Tests
    {
/*
U3-L02_1:Practitioner - Verify Interactions and All Search Parameters
U3-L02_1_T01:metadata: Verify name as Search Param. for Practitioner
U3-L02_1_T02:metadata: Verify family as Search Param. for Practitioner
U3-L02_1_T03:metadata: Verify given as search parameter for Practitioner
U3-L02_1_T04:metadata: Verify _id as search parameter for Practitioner
U3-L02_1_T05:metadata: Verify email as search parameter for Practitioner
U3-L02_1_T06:metadata: Verify telecom as search parameter for Practitioner
U3-L02_1_T07:metadata: Verify identifier as search parameter for Practitioner
U3-L02_1_T08:metadata: Verify birthdate is NOT a search parameter for Practitioner
U3-L02_1_T09:metadata: Verify Practitioner/read as Interaction
U3-L02_1_T10:metadata: Verify Practitioner/search-type as Interaction
*/

        [Fact]
        public void L02_1_T01_Verify_Practitioner_Search_Parameter_name()

        {
            var result=L02_1_testrunner.L02_1_T01();
            var expected = "String";
            Assert.Equal(expected, result);
            
        }
      [Fact]
      public void L02_1_T02_Verify_Practitioner_Search_Parameter_family()

        {
            var result=L02_1_testrunner.L02_1_T02();
            var expected = "String";
            Assert.Equal(expected, result);
            
        }
        [Fact]
      public void L02_1_T03_Verify_Practitioner_Search_Parameter_given()

        {
            var result=L02_1_testrunner.L02_1_T03();
            var expected = "String";
            Assert.Equal(expected, result);
            
        }
        [Fact]
      public void L02_1_T04_Verify_Practitioner_Search_Parameter__id()

        {
            var result=L02_1_testrunner.L02_1_T04();
            var expected = "Token";
            Assert.Equal(expected, result);
            
        }
         [Fact]
      public void L02_1_T05_Verify_Practitioner_Search_Parameter_email()

        {
            var result=L02_1_testrunner.L02_1_T05();
            var expected = "Token";
            Assert.Equal(expected, result);
            
        }
         [Fact]
      public void L02_1_T06_Verify_Practitioner_Search_Parameter_telecom()

        {
            var result=L02_1_testrunner.L02_1_T06();
            var expected = "Token";
            Assert.Equal(expected, result);
            
        }

         [Fact]
      public void L02_1_T07_Verify_Practitioner_Search_Parameter_identifier()

        {
            var result=L02_1_testrunner.L02_1_T07();
            var expected = "Token";
            Assert.Equal(expected, result);
            
        }

[Fact]
    public void L02_1_T08_Verify_Practitioner_Search_Parameter_birthdate()

        {
            var result=L02_1_testrunner.L02_1_T08();
            var expected = "notsupported";
            Assert.Equal(expected, result);
            
        }
        [Fact]
        public void L02_1_T09_Verify_Practitioner_Interaction_read()

        {
            var result=L02_1_testrunner.L02_1_T09();
            var expected = "Read";
            Assert.Equal(expected, result);
            
        }
        [Fact]
        public void L02_1_T10_Verify_Practitioner_Interaction_searchtype()

        {
            var result=L02_1_testrunner.L02_1_T10();
            var expected = "SearchType";
            Assert.Equal(expected, result);
            
        }

}
}

