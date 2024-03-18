using System;
using fhir_server_entity_model;
using System.Collections.Generic;
using System.Linq;

namespace fhir_server_dataaccess
{
    public static class PatientDataAccess
    {
        
        public static  List<LegacyPerson> GetAllPatients(string SpecificPatientId = null)
        {
            var people = new List<LegacyPerson>();
            LegacyAPIAccess.GetLegacyData();
            var lp=LegacyAPIAccess.LegacyPerson;
            if (lp!=null)
            {
                var max=lp.Length;
                for (var i = 0; i < max; i++)
                {
                    var item=lp[i];
                    people.Add(item);
                }
            }
            return people;
        }
        public static List<LegacyPersonIdentifier> GetPersonDocType(long personId)
        {
            LegacyAPIAccess.GetLegacyData();
           
            var docTypes = new List<LegacyPersonIdentifier>();
            var lpi=LegacyAPIAccess.LegacyPersonIdentifiers;
            if (lpi!=null)
            {
                var max=lpi.Length;
                
                for (var i = 0; i < max; i++)
                {
                    if (lpi[i].prsn_id==personId)
                    {docTypes.Add(lpi[i]);}
                }
            }
            return docTypes;
        }
        public static List<LegacyPerson> GetPerson(List<LegacyFilter> Criteria) 
        {
            LegacyAPIAccess.GetLegacyData();
            var people = new List<LegacyPerson>();
            
            var lp=LegacyAPIAccess.LegacyPerson;
            var max=lp.Length;
            for (var i = 0; i < max; i++)
            {
                var item=lp[i];
                var include=true;
                foreach ( var c in Criteria)
                {
                    switch  (c.criteria)
                    {
                        case LegacyFilter.field.name:
                            {
                                var fullname=item.PRSN_FIRST_NAME.ToLower()+" "+item.PRSN_LAST_NAME.ToLower();
                                if (!(fullname.Contains(c.value.ToLower())))
                                {
                                    include=false;
                                }
                                break;
                            }
                        case LegacyFilter.field.family:
                            if (!(item.PRSN_LAST_NAME.ToLower().Contains(c.value.ToLower())))
                            {
                              include=false;      
                            }
                            break;
                        case LegacyFilter.field.given:
                            if (!(item.PRSN_FIRST_NAME.ToLower().Contains(c.value.ToLower())))
                            {
                              include=false;      
                            }
                            break;
                        case LegacyFilter.field.id:
                            if (item.PRSN_ID.ToString()!=c.value)
                            {
                                include=false;
                            }
                            break;
                        case LegacyFilter.field._id:
                            
                            if (item.PRSN_ID.ToString()!=c.value)
                            
                            {
                                include=false;
                            }
                            break;
                        case LegacyFilter.field.birthdate:
                            var which_date=c.value;
                            
                            if (!(item.PRSN_BIRTH_DATE.ToString()==which_date))
                            {
                                include=false;
                            }
                            break;
                        case LegacyFilter.field.identifier:
                            var search_ident=c.value;
                            var personIdentifiers = fhir_server_dataaccess.PatientDataAccess.GetPersonDocType(item.PRSN_ID);
                            var ident_found=false;
                            foreach (var docType in personIdentifiers)
                            {
    
                                var the_System = "http://fhirintermediatecourse.org/"+fhir_server_dataaccess.LegacyAPIAccess.getLegacyIdentifierCode(docType.identifier_type_id);
                                var the_Value = docType.value;
                                var the_ident=the_System+"|"+the_Value;
                                Console.WriteLine(the_ident);
                                Console.WriteLine(search_ident);
                                
                                if (the_ident==search_ident)
                                {
                                    ident_found=true;
                                    break;
                                }
                            }
                            if (!ident_found){include=false;}
                            break;
                        case LegacyFilter.field.gender:
                            if (item.PRSN_GENDER.ToLower()!=c.value.ToLower())
                            {
                                include=false;
                            }
                            break;
                    default:
                        break;
                    }
                    if (!include){break;}

                }
            
                if (include) people.Add(item);
            }
            
            return people;
        }
       
    }
}

