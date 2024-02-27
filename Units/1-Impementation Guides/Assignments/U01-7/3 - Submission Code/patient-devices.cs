using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Fhir.Metrics;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Language;
using Hl7.Fhir.Language.Debugging;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Navigation;
using Hl7.Fhir.Specification.Snapshot;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Specification.Tests;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Functions;
using Hl7.FhirPath.Parser;
using Hl7.FhirPath.Sprache;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

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