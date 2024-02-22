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

/// Notes:
/// 1. Using latest HL7.FHIR.R4 NuGet Package v5.6.1
/// 2. Syncronous calls are marked obsolete and discouraged.
///    Using Async to follow protocal
/// 3. Framework: .NET 8
/// 4. Coding Environment: LinqPad8 (https://www.linqpad.net/)
/// 
/// Also runnable here: https://dotnetfiddle.net/uTolF9
static async System.Threading.Tasks.Task Main()
{
	var patientId = "X12984";
	//var patientId = "1940";
	var goals = await GetPatientGoalsAsync(patientId);
	Console.WriteLine(goals);	
}

/// <summary>
/// Example: http://fhirserver.hl7fundamentals.org/fhir/Goal?patient=X12984
/// 
/// Exclude: 'subject' from mandatory elements as it's a search parameter.
/// Return Format: code|description|date|target
/// 
/// Assignment is expecting:
/// - Improve and maintenance of optimal foot health is missing
/// - Reduce sodium is missing
/// - Maintain blood pressure below 140 / 90 mm[Hg] is missing
/// - Hemoglobin A1c total in Blood is missing
/// - Glucose[Mass / volume] in Blood is missing
/// </summary>
public static async Task<string> GetPatientGoalsAsync(string patientId)
{
	const string FHIREndpoint = "http://fhirserver.hl7fundamentals.org/fhir";
	
	using var client = new FhirClient(FHIREndpoint);
	
	var searchParams = new SearchParams();
	searchParams.Add("patient", patientId);
	
	var response = await client.SearchAsync<Goal>(searchParams);
	
	if(response.Total <= 0) { return "No goals found."; }
	
	var goals = new StringBuilder();
	foreach(var entry in response.Entry)
	{
		var goal = (Goal)entry.Resource;
		
		string code = default;
		if(goal.LifecycleStatus.HasValue)
		{
			code = goal.LifecycleStatus.Value.ToString();
		}

		var description = goal.Description.Text;
		string startDate = "No start date";
		if(goal.Start != default)
		{
			startDate = goal.Start.ToString();
		}
		var targetDate = goal.Target[0].Due;

		var currentGoal = $"{code}|{description}|{startDate}|{targetDate}";
		goals.AppendLine(currentGoal);
	}
	
	return goals.ToString();
}
