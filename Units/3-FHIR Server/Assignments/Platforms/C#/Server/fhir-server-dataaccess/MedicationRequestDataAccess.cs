using System;
using fhir_server_entity_model;
using System.Collections.Generic;
using System.Text;

namespace fhir_server_dataaccess
{
    public static class MedicationRequestDataAccess
    {
        
        public static  List<LegacyRx> GetAllMedicationRequests(string SpecificRxId = null)
        {
            var rxs = new List<LegacyRx>();
            LegacyAPIAccess.GetLegacyData();
            var lr=LegacyAPIAccess.LegacyRxs;
            if (lr!=null)
            {
                var max=lr.Length;
                for (var i = 0; i < max; i++)
                {
                    var item=lr[i];
                    rxs.Add(item);
                }
            }
            return rxs;
        }
        public static List<LegacyRx> GetMedicationRequest(List<LegacyFilter> Criteria) 
        {
            LegacyAPIAccess.GetLegacyData();
            var rxs = new List<LegacyRx>();
            var compoundId="";
            var thisId="";
                
            var lp=LegacyAPIAccess.LegacyRxs;
            var max=lp.Length;
            for (var i = 0; i < max; i++)
            {
                var item=lp[i];
                var include=true;
                foreach ( var c in Criteria)
                {
                    Console.WriteLine(c.criteria.ToString());
                    switch  (c.criteria)
                    {
                        case LegacyFilter.field.id:
                            compoundId = item.patient_id.ToString() + "-"+item.prescriber_id.ToString()+"-"+item.prescription_date.ToString().Replace("-","")+"-"+item.rxnorm_code.ToString();
                            thisId = Convert.ToBase64String(Encoding.UTF8.GetBytes(compoundId));
                            Console.WriteLine(thisId);
                            Console.WriteLine(c.value);
                            if (c.value!=thisId)    
                            {
                                include=false;
                            }
                            break;
                        case LegacyFilter.field._id:
                            
                            compoundId = item.patient_id.ToString() + "-"+item.prescriber_id.ToString()+"-"+item.prescription_date.ToString()+"-"+item.rxnorm_code.ToString();
                            thisId = Convert.ToBase64String(Encoding.UTF8.GetBytes(compoundId));
                            if (c.value!=thisId)    
                            {
                                include=false;
                            }
                            break;
                        case LegacyFilter.field.subject:
                            var patientId="Patient/"+item.patient_id.ToString();
                            if (c.value!=patientId)
                            {
                                include=false;
                            }
                            break;
                    default:
                        break;
                    }
                    if (!include){break;}

                }
            
                if (include) rxs.Add(item);
            }
            
            return rxs;
        }
       
    }
}

