using RestSharp;
using Shiny.Jobs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJob
{
    public class RepeatedTask : IJob
    {
        const string SAMPLE_URL = "HTTPS//SOME.SAMPLE.URL";
        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            //if cancellation is requested then do nothing
            if (cancelToken.IsCancellationRequested)
            {
                return false;
            }
            // Logic when job should run
            var runJob = false;
            // if you want to run the job only two times a day or at specific time you can do that
            DateTime dateTime9 = DateTime.Today;
            dateTime9 = dateTime9.AddHours(9);

            DateTime dateTime4 = DateTime.Today;
            dateTime4 = dateTime4.AddHours(15);
            // Jobinfo provides when the job is last ran
            if (jobInfo.LastRunUtc == null) // job has never run
            {
                runJob = true;
            }
            else if ((jobInfo.LastRunUtc?.ToLocalTime().CompareTo(dateTime4)) >= 0)
            {
                runJob = true;  // its been at least 4 o'clcock since the last run

            }
            else if ((jobInfo.LastRunUtc?.ToLocalTime().CompareTo(dateTime9)) >= 0)
            {
                runJob = true;
            }

            if (runJob)
            {
                //Your task 
                // you can also inject your service/ dependency in the Job
                var client = new RestClient(SAMPLE_URL);

                var request = new RestRequest(Method.POST);
                request.AddHeader("User-Agent", "Mozilla/4.0 (Macintosh; Intel Mac OS X 10.13; rv:71.0) Gecko/20100101 Firefox/" + jobInfo.PeriodicTime?.Seconds);
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Accept-Language", "en-US,en;q=0.5");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                IRestResponse response = await client.ExecuteAsync(request);
                Console.WriteLine("response :-" + response.Content);


                return true;
            }
            return false;
        }
    }
}
