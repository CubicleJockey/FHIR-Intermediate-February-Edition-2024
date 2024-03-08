using System;

namespace fhirclient_dotnet_submission
{
    class Program
    {
        static void Main(string[] args)
        {
            var submission=new SubmissionCreator();
            var filename=submission.CreateSubmission();
            Console.WriteLine(filename);
        }
    }
}
