using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

namespace PatientAllergies
{
    public partial class FormPatientAllergies : Form
    {
        public FormPatientAllergies()
        {
            InitializeComponent();
        }

        private void BtnSearchPatient_Click(object sender, EventArgs e)
        {
            SearchPatients();
        }

        private void WorkingMessage()
        {
            lblErrorMessage.Text = "Working...";
            this.UseWaitCursor = true;
        }

        private void SearchAllergies(string PatientId)
        {
            WorkingMessage();
            listAllergies.Items.Clear();
            string FHIR_EndPoint = this.txtFHIREndpoint.Text.ToString();
            var client = new Hl7.Fhir.Rest.FhirClient(FHIR_EndPoint);

            try
            {

                var p = new Hl7.Fhir.Rest.SearchParams();
                p.Add("patient", PatientId);

                var results = client.Search<AllergyIntolerance>(p);
                this.UseWaitCursor = false;
                lblErrorMessage.Text = "";
                while (results != null)
                {
                    if (results.Total == 0) lblErrorMessage.Text = "No allergies found";

                    foreach (var entry in results.Entry)
                    {

                        var Alrgy = (AllergyIntolerance)entry.Resource;
                        string Content = Alrgy.Code.Coding[0].Display
                            + " / " +Alrgy.VerificationStatus.Coding[0].Code
                            + " (" + Alrgy.ClinicalStatus.Coding[0].Code + ")";
                        listAllergies.Items.Add(Content);
                        

                    }
                    // get the next page of results
                    results = client.Continue(results);
                }
                }
            catch(Exception err)
            {
                lblErrorMessage.Text = "Error:" + err.Message.ToString();

            }
            if (lblErrorMessage.Text != "") { lblErrorMessage.Visible = true; }

        }
        private void SearchPatients()
        {
            WorkingMessage();
            listCandidates.Items.Clear();
            string FHIR_EndPoint = this.txtFHIREndpoint.Text.ToString();
            var client = new Hl7.Fhir.Rest.FhirClient(FHIR_EndPoint);
            client.OnBeforeRequest += (object sender, BeforeRequestEventArgs e) =>
            {
                // Replace with a valid bearer token for the server
                e.RawRequest.Headers.Add("Authorization", "Bearer ya29.QQIBibTwvKkE39hY8mdkT_mXZoRh7Ub9cK9hNsqrxem4QJ6sQa36VHfyuBe");
            };
            try
            {
                string family = txtFamilyName.Text.ToString();
                string given = txtGivenName.Text.ToString();
                var p = new Hl7.Fhir.Rest.SearchParams();
                p.Add("family", family);
                p.Add("given", given);
                var results = client.Search<Patient>(p);
                this.UseWaitCursor = false;
                lblErrorMessage.Text = "";

                while (results != null)
                {
                    if (results.Total == 0) lblErrorMessage.Text = "No patients found";
                    btnShowAllergies.Enabled = true;
                    foreach (var entry in results.Entry)
                    {

                        var Pat = (Patient)entry.Resource;
                        string Fam = Pat.Name[0].Family;
                        string Giv = Pat.Name[0].GivenElement[0].ToString();
                        string ideS = Pat.Identifier[0].System;
                        string ideV = Pat.Identifier[0].Value;
                        string Content =  Fam+ " " + Giv+  " (" +ideS + "-" + ideV+")";
                        ListViewItem l = new ListViewItem
                        {
                            Text = Content,
                            Tag = entry.Resource.Id
                        };
                        listCandidates.Items.Add(l);
                        
                    }

                    // get the next page of results
                    results = client.Continue(results);
                }
            }
            catch (Exception err)
            {
                lblErrorMessage.Text = "Error:" + err.Message.ToString();
            }
            if (lblErrorMessage.Text != "") { lblErrorMessage.Visible = true; }

        }

        private void BtnShowAllergies_Click(object sender, EventArgs e)
        {
            ListViewItem l = (listCandidates.SelectedItem as ListViewItem);
            string patientId = l.Tag.ToString();
            SearchAllergies(patientId);
        }

        private void lblErrorMessage_Click(object sender, EventArgs e)
        {

        }
    }
}
