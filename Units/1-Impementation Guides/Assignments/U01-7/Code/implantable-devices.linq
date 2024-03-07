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
	Assignment: U01-7: US Core FHIR R4 Implantable Device (C#)
	Student: 	André Davis
	Framework: 	.NET 8
	NuGet: 		HL7.FHIR.R4 v5.6.1
	
	Resource Documentation: https://www.hl7.org/fhir/device.html
	Resource Profile - US Core Implantable Device Profile: https://www.hl7.org/fhir/us/core/StructureDefinition-us-core-implantable-device.html
	
		UDI component							US Core Implantable Device Profile element
		-------------							------------------------------------------
		UDI HRF string							Device.udiCarrier.carrierHRF
		DI										Device.udiCarrier.deviceIdentifier
		Manufacture date (UDI-PI element)		Device.manufactureDate
		Expiration dat (UDI-PI elemente)		Device.expirationDate
		Lot number (UDI-PI element)				Device.lotNumber
		Serial number (UDI-PI element)			Device.serialNumber
		Distinct identifier (UDI-PI element)	Device.distinctIdentifier
*/
static async System.Threading.Tasks.Task Main(string[] args)
{
	//var patientId = "1";
	var patientId = "X12984";
	var deviceList = await GetPatientDevicesAsync(patientId);
	Console.WriteLine(deviceList);
}


//NOTE: Valid search parameters for this search are: [_id, _language, _lastUpdated, device-name, identifier, location, manufacturer, model, organization, patient, status, type, udi-carrier, udi-di, url]
public static async Task<string> GetPatientDevicesAsync(string patientId, string status = "active")
{
	const string FHIR_EndPoint = "http://fhirserver.hl7fundamentals.org/fhir";

	using var client = new FhirClient(FHIR_EndPoint);
	
	var searchParams = new SearchParams();
	searchParams.Add("patient", patientId);
	searchParams.Add("status", status);
	
	var fhirBundle = await client.SearchAsync<Device>(searchParams);
	
	var output = new StringBuilder();
	while (fhirBundle != default)
	{
		if (fhirBundle.Total <= 0) { output.AppendLine("No devices found"); };

		foreach (var entry in fhirBundle.Entry)
		{
			var device = (Device)entry.Resource;
			
			if(!device.Status.HasValue) { continue; }
			//if(device.UdiCarrier.Count <= 0) { continue; }
			if(device.UdiCarrier.Count > 0)
			{
				output.Append($"{device.UdiCarrier[0].CarrierHRF}|");
				output.Append($"{device.UdiCarrier[0].DeviceIdentifier}|");
			}

			output.Append($"{device.ManufactureDate}|");
			output.Append($"{device.ExpirationDate}|");
			output.Append($"{device.LotNumber}|");
			output.Append($"{device.SerialNumber}|");
			output.Append($"{device.DistinctIdentifier}|");
			output.Append($"{device.Type.Coding[0].Display}|");
			
			//if (device.Status.HasValue)
			//{
				output.AppendLine($"{device.Status}");
			//}
			//else { output.AppendLine(); }
		}

		// get the next page of results
		if (fhirBundle.NextLink != default)
		{
			fhirBundle = await client.ContinueAsync(fhirBundle);
		}
		else { fhirBundle = default; }

	}
	return output.ToString();
}
