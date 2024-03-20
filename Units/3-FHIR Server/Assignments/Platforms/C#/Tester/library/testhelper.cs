using System;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
namespace fhirserver_dotnet_library
{

    public static class testhelper
    {
        public static string MedicationRequestFindRequesterDisplay(string server, string id)
        {
            var result = "<<notfound>>";
            var mr = MedicationRequestGet(server, id);
            if (mr.Requester != null)
            {
                if (mr.Requester.Display != null)
                {
                    result = mr.Requester.Display;
                }
            }
            return result;
        }
        public static string MedicationRequestFindWarningSIG(string server, string id)
        {
            var result = "<<warningnotfound>>";
            var mr = MedicationRequestGet(server, id);
            if (mr.DosageInstruction.Count > 0)
            {
                if (mr.DosageInstruction[0].Text != "")
                {
                    result =  mr.DosageInstruction[0].Text;

                }
                if (mr.DosageInstruction.Count > 1)
                {
                    if (mr.DosageInstruction[1].Text != "")
                    {
                        result =  mr.DosageInstruction[1].Text;
                    }
                }
            }
            return result;
        }

        public static string MedicationRequestGetAndValidate(string server, string id, string ValidationServer)
        {
            var result = "";
            var mr = MedicationRequestGet(server, id);
            var fjs = new FhirJsonSerializer();
            var MyMedicationRequest = fjs.SerializeToString(mr);

            if (MyMedicationRequest != "")
            {
                result = MedicationRequestValidate_USCORE(MyMedicationRequest, ValidationServer);
            }
            return result;
        }
        public static string MedicationRequestValidate_USCORE(string JsonMedicationRequest, string server)
        {
            var aux = "";
            var o = new MedicationRequest();
            var parser = new FhirJsonParser();

            try
            {
                o = parser.Parse<MedicationRequest>(JsonMedicationRequest);
            }
            catch
            {
                aux = "Error:Invalid_MedicationRequest_Resource";
            }

            if (aux == "")
            {
                var client = new FhirClient(server);
                var profile = new FhirUri("http://hl7.org/fhir/us/core/StructureDefinition/us-core-medicationrequest");

                var inParams = new Parameters();
                inParams.Add("resource", o);
                var bu = client.ValidateResource(o);
                if (bu.Issue[0].Details.Text != "All OK")
                {
                    aux = "Error:" + bu.Issue[0].Details.Text;
                }
                else
                {
                    aux="All OK";
                }
            }
            return aux;

        }


        public static MedicationRequest MedicationRequestGet(string serverEndpoint, string Id)
        {

            MedicationRequest result;

            var client = new FhirClient(serverEndpoint);
            try
            {
                var mr = client.Read<MedicationRequest>(Id);
                result = (MedicationRequest)mr;
            }
            catch (Exception e)
            {

                throw (e);
            }

            return result;

        }
        public static string GetPractitionersAll(string ServerEndpoint)
        {
            var result = PractitionerSearch(ServerEndpoint, string.Empty, string.Empty);
            return result;
        }

        public static string PractitionerSearch(string ServerEndpoint, string SearchParameter, string Value)
        {
            var result = "";
            var client = new FhirClient(ServerEndpoint);
            Bundle bu;
            try
            {
                if (SearchParameter != "")
                {
                    var q = new SearchParams(SearchParameter, Value);
                    bu = client.Search<Practitioner>(q);
                }
                else
                { bu = client.Search<Practitioner>(); }

                var totP = 0;
                var totNP = 0;
                foreach (var ent in bu.Entry)
                {
                    var pa = (Practitioner)ent.Resource;

                    totP = totP + 1;
                }
                result = totP.ToString() + ":" + totNP.ToString();
            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;
        }
        public static string PractitionerGetById(string ServerEndpoint, string Id)
        {

            var result = "<<NOT EXISTING>>";

            var client = new FhirClient(ServerEndpoint);
            try
            {
                var pa = client.Read<Practitioner>(Id);
                var MyPractitioner = pa.Name[0].Family + " " + pa.Name[0].GivenElement[0].Value.ToString();
                if (pa.Name[0].GivenElement.Count > 1)
                {
                    MyPractitioner = MyPractitioner + " " + pa.Name[0].GivenElement[1].Value.ToString();
                }
                result = MyPractitioner;
            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;

        }
        public static string CapabilityCheckInteraction(string ServerEndpoint, string resource, string interaction)
        {
            var result = "";

            var cs = GetCapabilityStatement(ServerEndpoint);
            var csr = cs.Rest[0];
            Console.WriteLine(csr.Resource.Count);

            var rc = csr.Resource.Count;
            for (var i = 0; i < rc; i++)
            {
                var csrc = csr.Resource[i];
                if (csrc.Type.ToString() == resource)
                {


                    var intc = csrc.Interaction.Count;
                    Console.WriteLine("Intercount");
                    Console.WriteLine(intc);
                    for (var j = 0; j < intc; j++)
                    {

                        var cinc = csrc.Interaction[j];
                        Console.WriteLine("code");

                        Console.WriteLine(cinc.Code.ToString());

                        if (cinc.Code.ToString() == interaction)
                        {
                            result = interaction;
                            break;
                        }
                    }
                    break;

                }
            }
            return result;
        }
        public static string CapabilityCheckParameterType(string ServerEndpoint, string resource, string name)
        {
            var result = "notsupported";
            var cs = GetCapabilityStatement(ServerEndpoint);
            var csr = cs.Rest[0];
            Console.WriteLine(csr.Resource.Count);

            var rc = csr.Resource.Count;
            for (var i = 0; i < rc; i++)
            {
                var csrc = csr.Resource[i];
                if (csrc.Type.ToString() == resource)
                {

                    var spc = csrc.SearchParam.Count;
                    for (var j = 0; j < spc; j++)
                    {

                        var csrp = csrc.SearchParam[j];
                        if (csrp.Name.ToString() == name)
                        {
                            result = csrp.Type.ToString();
                            break;
                        }
                    }
                    break;

                }

            }
            return result;
        }
        private static CapabilityStatement GetCapabilityStatement(string ServerEndPoint)
        {
            var client = new FhirClient(ServerEndPoint);
            var cs = client.CapabilityStatement();
            return cs;
        }
        public static string PatientSearchByTelecom(string ServerEndpoint, string telecom)
        {
            var result = "<<NOT EXISTING>>";

            var client = new FhirClient(ServerEndpoint);
            try
            {
                var q = new SearchParams("telecom", telecom);
                var bu = client.Search<Patient>(q);
                foreach (var ent in bu.Entry)
                {
                    var pa = (Patient)ent.Resource;
                    var MyPatient = pa.Name[0].Family + " " + pa.Name[0].GivenElement[0].Value.ToString();
                    if (pa.Name[0].GivenElement.Count > 1)
                    {
                        MyPatient = MyPatient + " " + pa.Name[0].GivenElement[1].Value.ToString();
                    }
                    result = MyPatient;
                }

            }
            catch (Exception e)
            {

                result = e.Message;
            }

            return result;
        }
        public static string PatientSearchByEmail(string ServerEndpoint, string email)
        {
            var result = "<<NOT EXISTING>>";

            var client = new FhirClient(ServerEndpoint);

            var q = new SearchParams("email", email);
            var bu = client.Search<Patient>(q);

            foreach (var ent in bu.Entry)
            {
                var pa = (Patient)ent.Resource;
                var MyPatient = pa.Name[0].Family + " " + pa.Name[0].GivenElement[0].Value.ToString();
                if (pa.Name[0].GivenElement.Count > 1)
                {
                    MyPatient = MyPatient + " " + pa.Name[0].GivenElement[1].Value.ToString();
                }
                result = MyPatient;
            }
            return result;
        }


    }
}
