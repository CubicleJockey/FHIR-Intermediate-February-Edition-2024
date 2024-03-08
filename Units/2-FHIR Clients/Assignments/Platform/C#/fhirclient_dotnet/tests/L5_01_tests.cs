using Xunit;
using fhirclient_dotnet;

namespace fhirclient_dotnet_tests
{
 
    public class L05_1_ExpandValueset_Tests
    {
        //L05_1_T01: Term/Filter not exists
        //L05_1_T02: Term/Filter exists
        
        
        [Fact]

        public  void L05_1_T01_ExpandFilterNonExistingTerm()
    
        {
             var c=new MyConfiguration();
            var server=c.TerminologyServerEndpoint;

                var ExpTerms="Error:ValueSet_Filter_Not_Found";
            var url="http://snomed.info/sct?fhir_vs=isa/73211009";
            var filter="diaxetes";
            var fsh=new TerminologyService();
            var rm= fsh.ExpandValueSetForCombo(server,
                                              url,
                                              filter);
            Assert.True(ExpTerms==rm,ExpTerms+"!="+rm);
            
        }

    

        [Fact]

        public  void L05_1_T02_ExpandFilterExistingTerm()
        {

            var c=new MyConfiguration();
            var server=c.TerminologyServerEndpoint;
            var ExpTerms="5368009|Drug-induced diabetes mellitus\n";
            
            var url="http://snomed.info/sct?fhir_vs=isa/73211009";
            var filter="Drug-induced diabetes";
            var fsh=new TerminologyService();
            var rm= fsh.ExpandValueSetForCombo(
                     server,
                     url,
                     filter);
            Assert.True(ExpTerms==rm,ExpTerms+"!="+rm);
            
        
        }
    }
}
