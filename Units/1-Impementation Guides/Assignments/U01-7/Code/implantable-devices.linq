<Query Kind="Program">
  <NuGetReference>Hl7.Fhir.R4</NuGetReference>
  <Namespace>Fhir.Metrics</Namespace>
  <Namespace>Hl7.Fhir.ElementModel</Namespace>
  <Namespace>Hl7.Fhir.ElementModel.Types</Namespace>
  <Namespace>Hl7.Fhir.FhirPath</Namespace>
  <Namespace>Hl7.Fhir.Introspection</Namespace>
  <Namespace>Hl7.Fhir.Language</Namespace>
  <Namespace>Hl7.Fhir.Language.Debugging</Namespace>
  <Namespace>Hl7.Fhir.Model</Namespace>
  <Namespace>Hl7.Fhir.Rest</Namespace>
  <Namespace>Hl7.Fhir.Serialization</Namespace>
  <Namespace>Hl7.Fhir.Specification</Namespace>
  <Namespace>Hl7.Fhir.Specification.Navigation</Namespace>
  <Namespace>Hl7.Fhir.Specification.Snapshot</Namespace>
  <Namespace>Hl7.Fhir.Specification.Source</Namespace>
  <Namespace>Hl7.Fhir.Specification.Summary</Namespace>
  <Namespace>Hl7.Fhir.Specification.Terminology</Namespace>
  <Namespace>Hl7.Fhir.Specification.Tests</Namespace>
  <Namespace>Hl7.Fhir.Support</Namespace>
  <Namespace>Hl7.Fhir.Utility</Namespace>
  <Namespace>Hl7.Fhir.Validation</Namespace>
  <Namespace>Hl7.FhirPath</Namespace>
  <Namespace>Hl7.FhirPath.Expressions</Namespace>
  <Namespace>Hl7.FhirPath.Functions</Namespace>
  <Namespace>Hl7.FhirPath.Parser</Namespace>
  <Namespace>Hl7.FhirPath.Sprache</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Bson</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Schema</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

/*
	Student: 	André Davis
	Framework: 	.NET 8
	NuGet: 		HL7.FHIR.R4 v5.6.1
	
	Documentation: https://www.hl7.org/fhir/device.html
*/
static async System.Threading.Tasks.Task Main(string[] args)
{
	string patientId = "1";
	string deviceList = await GetPatientDevicesAsync(patientId);
	Console.WriteLine(deviceList);
}


//NOTE: Valid search parameters for this search are: [_id, _language, _lastUpdated, device-name, identifier, location, manufacturer, model, organization, patient, status, type, udi-carrier, udi-di, url]
public static async Task<string> GetPatientDevicesAsync(string patientId)
{
	const string FHIR_EndPoint = "http://fhirserver.hl7fundamentals.org/fhir";

	using var client = new FhirClient(FHIR_EndPoint);
	
	var searchParams = new SearchParams();
	searchParams.Add("patient", patientId);
	
	var fhirBundle = await client.SearchAsync<Device>(searchParams);
	
	var output = new StringBuilder();
	while (fhirBundle != default)
	{
		if (fhirBundle.Total <= 0) { return "No devices found"; };

		foreach (var entry in fhirBundle.Entry)
		{
			var device = (Device)entry.Resource;
			
			if (device.UdiCarrier.Count > 0)
			{
				output.Append($"{device.UdiCarrier[0].CarrierHRF}|{device.UdiCarrier[0].DeviceIdentifier}|");
			}
			if (device.Status.HasValue)
			{
				output.AppendLine($"{device.Status}|");
			}

			// get the next page of results
			if (fhirBundle.NextLink  != default)
			{
				fhirBundle = await client.ContinueAsync(fhirBundle);
			}
			else 
			{
				fhirBundle = default;
				break;
			}
		}

	}
	return output.ToString();
}
