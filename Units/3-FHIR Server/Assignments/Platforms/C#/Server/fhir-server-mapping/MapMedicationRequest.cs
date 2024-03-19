using Hl7.Fhir.Model;
using fhir_server_dataaccess;
using Hl7.Fhir.Serialization;
using System.Collections.Generic;
using fhir_server_entity_model;
using System.Text;
using System;

namespace fhir_server_mapping
{
    public static class MapMedicationRequest
    {
        
        private static string GetPatientRefDisplay(string PatientId)
        {
            var display="";
            var criteria=new List<LegacyFilter>();
            var item=new LegacyFilter
            {
                criteria = LegacyFilter.field._id,
                value = PatientId
            };
            criteria.Add(item);
            var p=PeopleDataAccess.GetPerson(criteria) ;
            if (p.Count>0)
            {
                display=p[0].PRSN_LAST_NAME+" "+p[0].PRSN_FIRST_NAME;
                if (p[0].PRSN_SECOND_NAME!=""){ display=display+" "+p[0].PRSN_SECOND_NAME;}
            }
            return display;
        }
        public static MedicationRequest GetFHIRMedicationRequestResource(LegacyRx rx)
        {
                var mr = new MedicationRequest();
                var parser = new FhirJsonParser(new ParserSettings { AcceptUnknownMembers = false, AllowUnrecognizedEnums = false });
                var compoundId = rx.patient_id.ToString() + "-"+rx.prescriber_id.ToString()+"-"+rx.prescription_date.ToString().Replace("-","")+"-"+rx.rxnorm_code.ToString();
                mr.Id = Convert.ToBase64String(Encoding.UTF8.GetBytes(compoundId));
                var PatientDisplay=GetPatientRefDisplay(rx.patient_id.ToString());
                mr.Subject = new ResourceReference
                {
                    Reference = $"Patient/{rx.patient_id}",
                    Display = $"{PatientDisplay}"
                };
                mr.AuthoredOn = rx.prescription_date;
                var cc=new CodeableConcept("http://www.nlm.nih.gov/research/umls/rxnorm",rx.rxnorm_code,rx.rxnorm_display);
                mr.Medication=cc;
                var opioid=LegacyAPIAccess.CheckIfOpioid(rx.rxnorm_code);
                mr.Requester =new ResourceReference
                {
                    Reference =$"Practitioner/{rx.prescriber_id}"
                };
                var ds = new List<Dosage>();
                    
                if (!string.IsNullOrWhiteSpace(rx.sig))
                {
                    var item = new Dosage
                    {
                        Text = rx.sig.ToString()
                    };
                    ds.Add(item);
                    
                }
                if (ds.Count>0)
                {mr.DosageInstruction = ds;}
                mr.Meta = new Meta
                {
                    Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-medicationrequest" },
                };
                var GeneratedText = "Prescription for "+PatientDisplay+ " Date:"+rx.prescription_date+" of "+rx.rxnorm_code+":"+rx.rxnorm_display+" "+rx.sig;
                if (opioid) {GeneratedText=GeneratedText+" (opioid)";}
                mr.Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = $"<div xmlns=\"http://www.w3.org/1999/xhtml\"><p>"+GeneratedText+"</p></div>"
                };

            return mr;
        }
    }
}
