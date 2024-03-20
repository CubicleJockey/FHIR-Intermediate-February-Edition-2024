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

        //TODO: Add intent: proposal | plan | order | original-order | reflex-order | filler-order | instance-order | option
        public static MedicationRequest GetFHIRMedicationRequestResource(LegacyRx rx)
        {
                var medicationRequest = new MedicationRequest();
                var parser = new FhirJsonParser(new ParserSettings { AcceptUnknownMembers = false, AllowUnrecognizedEnums = false });
                var compoundId = $"{rx.patient_id}-{rx.prescriber_id}-{rx.prescription_date.ToString().Replace("-", string.Empty)}-{rx.rxnorm_code}";
                medicationRequest.Id = Convert.ToBase64String(Encoding.UTF8.GetBytes(compoundId));
                var patientDisplay=GetPatientRefDisplay(rx.patient_id.ToString());
                medicationRequest.Subject = new ResourceReference
                {
                    Reference = $"Patient/{rx.patient_id}",
                    Display = $"{patientDisplay}"
                };
                
                medicationRequest.AuthoredOn = rx.prescription_date;
                var cc=new CodeableConcept("http://www.nlm.nih.gov/research/umls/rxnorm",rx.rxnorm_code,rx.rxnorm_display);
                medicationRequest.Medication=cc;
                
                var opioid=LegacyAPIAccess.CheckIfOpioid(rx.rxnorm_code);
                medicationRequest.Requester =new ResourceReference
                {
                    Reference =$"Practitioner/{rx.prescriber_id}"
                };
                var dosage = new List<Dosage>();
                    
                if (!string.IsNullOrWhiteSpace(rx.sig))
                {
                    var item = new Dosage
                    {
                        Text = rx.sig.ToString()
                    };
                    dosage.Add(item);
                    
                }

                //Add opioid warnings
                if (opioid)
                {
                    var item = new Dosage
                    {
                        Text = "WARNINGS - Limitations of use - Because of the risks associated with the use of opioids, [Product] should only be used in patients for whom other treatment options, including non-opioid analgesics, are ineffective, not tolerated or otherwise inadequate to provide appropriate management of pain"
                    };
                    dosage.Add(item);
                }
                
                if (dosage.Count > 0) { medicationRequest.DosageInstruction = dosage; }
                
                medicationRequest.Meta = new Meta
                {
                    Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-medicationrequest" },
                };
                var generatedText = $"Prescription for {patientDisplay} Date:{rx.prescription_date} of {rx.rxnorm_code}:{rx.rxnorm_display} {rx.sig}";

                if (opioid)
                {
                    generatedText += " (opioid)";
                }

                medicationRequest.Intent = MedicationRequest.medicationRequestIntent.Order;
                medicationRequest.Status = MedicationRequest.medicationrequestStatus.Unknown;
                
                medicationRequest.Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = $"<div xmlns=\"http://www.w3.org/1999/xhtml\"><p>"+generatedText+"</p></div>"
                };

            return medicationRequest;
        }
    }
}
