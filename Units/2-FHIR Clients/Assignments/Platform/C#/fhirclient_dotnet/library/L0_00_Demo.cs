// This module is a demo in C#/.NET core, created to understand different ways to query a FHIR Server
// and retrieve/create Patient Demographic Information and US Core/IPS conformant resources
// You are free to copy code from this demos to create the functions for your assignment submission.
// (copying from here is not mandatory, though)
// We will demo:
// 1) How to get a Patient's full name and address
// 2) How to get a US Core Race extension
// 3) How to read all US Core Condition resources for a patient
// 4) How to create a US Core Allergy conformant resource
// 5) How to read lab results from an IPS document for a patient
// 6) How to expand  a valueset using a terminology server
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace fhirclient_dotnet
{
    public class Demo
    {
        private static string GetGiven(Patient p)
        {
            var name = p.Name[0];
            var first = string.Empty;
            foreach (var m in name.Given)
            {
                first += m + " ";
            }

            return first.TrimEnd();
        }

        private string GetFamily(Patient p)
        {
            var te = p.Name[0].Family;
            return te.TrimEnd();
        }

        public string GetPatientFullNameAndAddress(
        string server,
        string patientidentifiersystem,
        string patientidentifiervalue
        )

        {
            var auxA = "";
            var auxN = "";

            var pa = FHIR_SearchByIdentifier(server, patientidentifiersystem, patientidentifiervalue);
            var aux = "Error:Patient_Not_Found"; //Default Error
            if (pa != null)
            {
                auxN = GetFamily(pa) + "," + GetGiven(pa);
                var add = pa.Address;
                foreach (var xad in add)
                {
                    var paddr = "";
                    var lns = xad.Line;
                    foreach (var l in lns)
                    {
                        paddr += l.ToString() + " ";
                    }
                    if (xad.City != null) { paddr = paddr + " - " + xad.City; }
                    if (xad.State != null) { paddr = paddr + " , " + xad.State; }
                    if (xad.Country != null) { paddr = paddr + " , " + xad.Country; }
                    if (xad.PostalCode != null) { paddr = paddr + " (" + xad.PostalCode + ")"; }
                    auxA = auxA + paddr + " / ";
                }
                if (auxN == "") { auxN = "-"; }
                if (auxA == "") { auxA = "-"; }
                aux = "Full Name:" + auxN + "\n" + "Address:" + auxA + "\n";
            }

            return aux;
        }



        private Patient FHIR_SearchByIdentifier(string ServerEndPoint, string IdentifierSystem, string IdentifierValue)
        {
            var o = new Patient();
            var client = new FhirClient(ServerEndPoint);
            var bu = client.Search<Patient>(new[]
                {"identifier="  +IdentifierSystem+"|"+IdentifierValue});
            if (bu.Entry.Count > 0)
            {
                o = (Patient)bu.Entry[0].Resource;
            }
            else
            { o = null; }
            return o;
        }



        private string GetRaceExtension(Patient p)
        {
            var auxt = "";
            var auxo = "";
            var auxd = "";
            var raceExtensionUrl = "http://hl7.org/fhir/us/core/StructureDefinition/us-core-race";
            var aux = "Error:No_us-core-race_Extension";
            var e = p.Extension;
            foreach (var ef in e)
            {
                if (ef.Url == raceExtensionUrl)
                {
                    aux = "";
                    foreach (var efs in ef.Extension)
                    {
                        switch (efs.Url)
                        {
                            case "text":
                                {
                                    auxt = "text|" + efs.Value.ToString() + "\n";
                                    break;
                                }
                            case "ombCategory":
                                {
                                    var c = (Coding)efs.Value;
                                    auxo = "code|" + c.Code + ":" + c.Display + "\n";
                                    break;
                                }
                            case "detailed":
                                {
                                    var c = (Coding)efs.Value;
                                    auxd += "detail|" + c.Code + ":" + c.Display + "\n";
                                    break;
                                }
                        }

                    }
                    aux += auxt;
                    aux += auxo;
                    aux += auxd;
                    break;
                }

            }
            if ((auxt == "") || (auxo == ""))
            {
                aux = "Error:Non_Conformant_us-core-race_Extension";
            }
            return aux;
        }

        public string GetUSCoreRace(
          string server,
          string patientidentifiersystem,
          string patientidentifiervalue
        )
        {
            var p = FHIR_SearchByIdentifier(server, patientidentifiersystem, patientidentifiervalue);
            var aux = "Error:Patient_Not_Found"; //Default Error
            if (p != null)
            {
                aux = GetRaceExtension(p);
            }
            return aux;
        }

        public string GetConditions
               (string ServerEndPoint,
                string IdentifierSystem,
                string IdentifierValue
                )
        {
            var patient = FHIR_SearchByIdentifier(ServerEndPoint, IdentifierSystem, IdentifierValue);
            var aux = "Error:Patient_Not_Found"; //Default Error

            if (patient != null)
            {
                aux = GetDetail(ServerEndPoint, patient);
            }
            return aux;
        }


        private string GetDetail(string server, Patient patient)
        {
            var aux = "Error:No_Conditions";
            var p = new Condition();
            var client = new FhirClient(server);
            var bu = client.Search<Condition>(new[]
             {"patient="  +patient.Id});
            if (bu.Entry.Count > 0)
            {
                aux = "";
                foreach (var e in bu.Entry)
                {
                    var oneP = (Condition)e.Resource;


                    var cStatus = oneP.ClinicalStatus.Coding[0].Display;
                    var cVerification = oneP.VerificationStatus.Coding[0].Display;
                    var cCategory = oneP.Category[0].Coding[0].Display;
                    var cCode = oneP.Code.Coding[0].Code + ":" + oneP.Code.Coding[0].Display;
                    aux += cStatus + "|" + cVerification + "|" + cCategory + "|" + cCode + "\n";
                }

            }
            return aux;
        }
        public string CreateUSCoreAllergyIntolerance(
          string server,
          string patientidentifiersystem,
          string patientidentifiervalue,
          string ClinicalStatusCode,
          string VerificationStatusCode,
          string CategoryCode,
          string CriticalityCode,
          string AllergySnomedCode,
          string AllergySnomedDisplay,
          string ManifestationSnomedCode,
          string ManifestationSnomedDisplay,
          string ManifestationSeverityCode
      )
        {

            var patient = FHIR_SearchByIdentifier(server, patientidentifiersystem, patientidentifiervalue);
            var aux = "Error:Patient_Not_Found"; //Default Error

            if (patient != null)
            {


                var Allergy = new AllergyIntolerance()
                {
                    Meta = new Meta()
                    {
                        VersionId = "1",
                        LastUpdated = DateTime.Today,
                        Profile = new List<string> { "http://hl7.org/fhir/us/core/StructureDefinition/us-core-allergyintolerance" }
                    },
                    Text = new Narrative()
                    {
                        Status = Narrative.NarrativeStatus.Generated,
                        Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\"><p>Redacted Text</p></div>"
                    },
                    ClinicalStatus = new CodeableConcept()
                    {
                        Coding = new List<Coding>()
                            {
                                new Coding{ System = "http://terminology.hl7.org/CodeSystem/allergyintolerance-clinical", Code = ClinicalStatusCode, Display = ClinicalStatusCode }
                            },
                    },

                    VerificationStatus =
                        new CodeableConcept()
                        {
                            Coding = new List<Coding>()
                            {
                                new Coding{ System ="http://terminology.hl7.org/CodeSystem/allergyintolerance-verification", Code = VerificationStatusCode, Display = VerificationStatusCode }
                            },

                        }
                    ,
                    Code = new CodeableConcept()
                    {
                        Coding = new List<Coding>()
                        {
                            new Coding() { System = "http://snomed.info/sct", Code = AllergySnomedCode, Display =AllergySnomedDisplay }
                        },
                        Text = AllergySnomedDisplay
                    },
                    Patient = new ResourceReference()
                    {

                        Reference = $"Patient/{patient.Id}",
                        Display = patient.Name[0].Family.ToString() + "," + GetGiven(patient)
                    },
                    Reaction = new List<AllergyIntolerance.ReactionComponent>()
                    {

                    }
                };

                var arc = new AllergyIntolerance.ReactionComponent();
                arc.Manifestation.Add(
                    new CodeableConcept()
                    {
                        Coding = new List<Coding>()
                            {
                                new Coding{ System = "http://snomed.info/sct"
                                , Code = ManifestationSnomedCode, Display = ManifestationSnomedDisplay }
                            }
                    }
                    );
                Allergy.Reaction.Add(arc);

                var s = new Hl7.Fhir.Serialization.FhirJsonSerializer();
                s.Settings.Pretty = false;
                aux = s.SerializeToString(instance: Allergy, summary: SummaryType.False);

            }
            return aux;

        }

        public string GetIPSLabResults
              (string ServerEndPoint,
               string IdentifierSystem,
               string IdentifierValue
               )
        {
            var patient = FHIR_SearchByIdentifier(ServerEndPoint, IdentifierSystem, IdentifierValue);
            var aux = "Error:Patient_Not_Found"; //Default Error

            if (patient != null)
            {
                aux = GetIPSLabResultsDetail(ServerEndPoint, patient);
            }
            return aux;
        }


        private string GetIPSLabResultsDetail(string server, Patient patient)
        {
            var aux = "Error:No_IPS";
            var p = new Bundle();
            var client = new FhirClient(server);
            var bu = client.Search<Bundle>(new[]
             {"composition.patient="  +patient.Id});
            //One or More documents
            if (bu.Entry.Count > 0)
            {

                foreach (var ed in bu.Entry)
                {
                    var OneDoc = (Bundle)ed.Resource;
                    if (OneDoc.Entry.Count > 0)
                    {
                        aux = "";
                        foreach (var e in OneDoc.Entry)
                        {
                            if (e.Resource.TypeName == "Observation")
                            {
                                var oneP = (Observation)e.Resource;
                                var m_category = oneP.Category[0].Coding[0].Code.ToLower();
                                if (m_category == "laboratory")
                                {
                                    //Must support for IPS Laboratory
                                    var m_code = "";
                                    var c = oneP.Code;
                                    var u_code = "";
                                    if (c.Coding.Count > 0)
                                    { u_code = c.Coding[0].Code; }
                                    //
                                    //This is null for the grouper
                                    //we only want the observations with results
                                    //
                                    if (u_code != "")
                                    {
                                        m_code = c.Coding[0].Code + ":" + c.Coding[0].Display;

                                    }
                                    if (m_code != "")
                                    {
                                        var m_result = "";
                                        if (oneP.Value.TypeName == "Quantity")
                                        {
                                            var m_Q = (Quantity)oneP.Value;
                                            m_result = m_Q.Value + " " + m_Q.Unit;
                                            ;
                                        }
                                        if (oneP.Value.TypeName == "String")
                                        {
                                            m_result = oneP.Value.ToString();
                                        }
                                        if (oneP.Value.TypeName == "CodeableConcept")
                                        {
                                            var m_C = (CodeableConcept)oneP.Value;
                                            m_result = m_C.Coding[0].Code + ":" + m_C.Coding[0].Display;
                                        }
                                        var m_status = oneP.Status.ToString();
                                        var m_datefo = oneP.Effective.ToString();

                                        aux += m_code + "|" + m_datefo + "|" + m_status + "|" + m_result + "\n";


                                    }

                                }
                            }



                        }
                    }
                }

            }
            if (aux == "") { aux = "Error:IPS_No_Lab_Results"; }
            return aux;
        }

        public String ExpandValueSetForCombo(
                 string EndPoint,
                 string Url,
                 string Filter

             )
        {

            var aux = "";
            ValueSet Result = null;
            try
            {

                var parser = new Hl7.Fhir.Serialization.FhirJsonParser();
                var Response = GetDataSync(EndPoint, Url, Filter);
                try
                {
                    Result = parser.Parse<ValueSet>(Response);
                }
                catch (Exception ex)
                {
                    aux = ex.Message;
                }


                aux = "";
                if (Result != null)
                {
                    foreach (var ec in Result.Expansion.Contains)
                    {
                        aux += ec.Code + "|" + ec.Display + "\n";
                    }
                    if (aux == "") { aux = "Error:ValueSet_Filter_Not_Found"; }

                }
            }
            catch (Exception ex)
            {
                aux = ex.ToString();
            }
            return aux;
        }
        String GetDataSync(String Server, String Url, String Filter)
        {
            var aux = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/fhir+json"));
                var CompleteUrl = Server + "/ValueSet/$expand?url=" + Url + "&filter=" + Filter;

                var response = client.GetAsync(CompleteUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    var responseString = responseContent.ReadAsStringAsync().Result;

                    aux = responseString;
                }
            }
            return aux;
        }



    }

}