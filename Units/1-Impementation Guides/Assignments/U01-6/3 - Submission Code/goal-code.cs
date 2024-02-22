using System;
using System.Text;
using System.Threading.Tasks;

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;


namespace FHIR_Goals
{
    class Program
    {
        /// Notes:
		/// 1. Using latest HL7.FHIR.R4 NuGet Package v5.6.1
		/// 2. Syncronous calls are marked obsolete and discouraged.
		///    Using Async to follow protocal
		/// 3. Framework: .NET 8
		/// 4. Coding Environment: LinqPad8 (https://www.linqpad.net/)
		///
		/// Code also here: https://dotnetfiddle.net/uTolF9
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
		/// - Improve and maintenance of optimal foot health
		/// - Reduce sodium
		/// - Maintain blood pressure below 140 / 90 mm[Hg]
		/// - Hemoglobin A1c total in Blood
		/// - Glucose[Mass / volume] in Blood
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
    }
}

