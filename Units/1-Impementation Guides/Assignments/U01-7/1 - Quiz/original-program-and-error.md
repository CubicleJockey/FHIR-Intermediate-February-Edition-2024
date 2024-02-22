# Original Code [DotNetFiddle](https://dotnetfiddle.net/EAuZTx)

> [Device Documentation](https://www.hl7.org/fhir/device.html)

```csharp
using System;
using Hl7.Fhir.Model;
namespace fic_device
{
    class Program
    {
        static void Main(string[] args)
        {
            string PatientId = "X12984";
            string DeviceList = GetPatientDevices(PatientId);
            Console.WriteLine(DeviceList);
        }

        public static string GetPatientDevices(string PatientId)

        {
            string FHIR_EndPoint = "http://fhirserver.hl7fundamentals.org/fhir";

            var client = new Hl7.Fhir.Rest.FhirClient(FHIR_EndPoint);
            var p = new Hl7.Fhir.Rest.SearchParams();
            p.Add("subject", PatientId);
            var results = client.Search<Device>(p);
            string output = "";
            while (results != null)
            {
                if (results.Total == 0) output = "No devices found";

                foreach (var entry in results.Entry)
                {
                    var Device = (Device)entry.Resource;
                    string Content = "";

                        if (Device.UdiCarrier.Count > 0)
                        {
                            Content = Device.UdiCarrier[0].CarrierHRF+"|"
                                     + Device.UdiCarrier[0].DeviceIdentifier + "|";
                                     
                        }
                        if (Device.Status.HasValue)
						{Content = Content + Device.Status+"|";
                        
                        output = output + Content + "\r\n";   
                }
                 // get the next page of results
                results = client.Continue(results);
            }
            
        }
		return output;
    }
	}
}
```

## Device Failure Message

```
Unhandled exception. Hl7.Fhir.Rest.FhirOperationException: Operation was unsuccessful because of a client error (BadRequest). OperationOutcome: <div xmlns="http://www.w3.org/1999/xhtml"><h1>Operation Outcome</h1><table border="0"><tr><td style="font-weight: bold;">ERROR</td><td>[]</td><td><pre>Unknown search parameter "subject" for resource type "Device". Valid search parameters for this search are: [_id, _language, _lastUpdated, device-name, identifier, location, manufacturer, model, organization, patient, status, type, udi-carrier, udi-di, url]</pre></td></tr></table></div>.
   at Hl7.Fhir.Rest.TaskExtensions.WaitResult[T](Task`1 task)
   at Hl7.Fhir.Rest.BaseFhirClient.Search[TResource](SearchParams q)
   at fic_device.Program.GetPatientDevices(String PatientId)
   at fic_device.Program.Main(String[] args)
Command terminated by signal 6
```