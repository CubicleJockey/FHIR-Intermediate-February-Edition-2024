using Xunit;
using fhirserver_dotnet_library;

namespace fhirserver_dotnet_tests
{

    public class L03_1_Tests
    {

        [Fact]
        public void L03_1_T01A_MedicationRequestDirectGetValidate()

        {
            var result = L03_3_testrunner.L03_3_T01A();
            var expected = "All OK";
            Assert.Equal(expected, result);

        }

        [Fact]
        public void L03_1_T02A_MedicationRequestPractitionerDisplay()

        {
            var result = L03_3_testrunner.L03_3_T02A();
            var expected = "Lewis Jerry";
            Assert.Equal(expected, result);

        }

        [Fact]
        public void L03_1_T03A_MessageForOpioids()

        {
            var c = new MyConfiguration();
            var result = L03_3_testrunner.L03_3_T03A();
            var expected = c.MSG_OpioidWarning;
            Assert.Equal(expected, result);

        }
    }
}