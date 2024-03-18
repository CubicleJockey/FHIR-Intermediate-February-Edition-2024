using System;
using fhir_server_entity_model;
using System.Net.Http;
using Newtonsoft.Json;
namespace fhir_server_dataaccess
{
 public static class LegacyAPIAccess
    {
        
        public static LegacyPerson[] LegacyPerson=null;
        public static LegacyPersonIdentifier[] LegacyPersonIdentifiers=null;
        public static LegacyIdentifierType[] LegacyIdentifierTypes=null;
        public static LegacyRx[] LegacyRxs=null;
        public static LegacyMed[] LegacyMeds=null;
        public static void GetLegacyData()
        {
            GetLegacyPersons();
            GetLegacyPersonIdes();
            GetLegacyIdentifierTypes();
            GetLegacyRxs();
            GetLegacyMeds();
        }
        private static void GetLegacyMeds()
        {
            if (LegacyMeds==null)
            {
                var url = "http://3.221.164.25:9080/meds";
                var client = new HttpClient();
                var response = client.GetStringAsync(url).Result;
                LegacyMeds= JsonConvert.DeserializeObject<LegacyMed[]>(response);
            }

        }
        
        private static void GetLegacyRxs()
        {
            if (LegacyRxs==null)
            {
                var url = "http://3.221.164.25:9080/rx";
                var client = new HttpClient();
                var response = client.GetStringAsync(url).Result;
                LegacyRxs= JsonConvert.DeserializeObject<LegacyRx[]>(response);
            }

        }
        public static bool CheckIfOpioid(String rxnorm_code)
        {
            GetLegacyData();
            var opioid=false;
            var ms=LegacyMeds; 
            for (var i = 0; i < ms.Length; i++)
            {
                var m = ms[i];
                if (m.code==rxnorm_code)
                {
                    if (m.opioid=="yes")
                    {
                        opioid=true;
                    }
                    break;
                }
            }
            return opioid;
        }
        private static void GetLegacyIdentifierTypes()
        {
            if (LegacyIdentifierTypes==null)
            {
                var url = "http://3.221.164.25:9080/identifier_type";
                var client = new HttpClient();
                var response = client.GetStringAsync(url).Result;
                LegacyIdentifierTypes = JsonConvert.DeserializeObject<LegacyIdentifierType[]>(response);
            }
        }
        public static String getLegacyIdentifierCode(long IdentifierTypeId)
        {
            var Code ="";
            GetLegacyData();
            if (LegacyIdentifierTypes!=null)
            {
                var max=LegacyIdentifierTypes.Length;
                for (var i = 0; i < max; i++)
                {
                    if (LegacyIdentifierTypes[i].identifier_type_id==IdentifierTypeId)
                    {
                        Code=LegacyIdentifierTypes[i].identifier_code;
                        break;
                    }

                }
            }
            return Code;
        
        }
        private static void GetLegacyPersonIdes()
        {
            if (LegacyPersonIdentifiers==null)
            {
                var url = "http://3.221.164.25:9080/person_identifier";
                var client = new HttpClient();
                var response = client.GetStringAsync(url).Result;
                LegacyPersonIdentifiers = JsonConvert.DeserializeObject<LegacyPersonIdentifier[]>(response);
            }
        }        
        private static void GetLegacyPersons ()
        {
            if (LegacyPerson==null)
            {
                var url = "http://3.221.164.25:9080/person";
                var client = new HttpClient();
                var response = client.GetStringAsync(url).Result;
                LegacyPerson = JsonConvert.DeserializeObject<LegacyPerson[]>(response);
            }
        }
     
    }
}
