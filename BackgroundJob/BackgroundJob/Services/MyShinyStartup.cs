

using Microsoft.Extensions.DependencyInjection;
using Shiny;
using Shiny.Jobs;
using System;

namespace BackgroundJob.Services
{
    public class MyShinyStartup : ShinyStartup
    {
        public static JobInfo RepeatedJob;
        public override void ConfigureServices(IServiceCollection services)
        {
            var job = new JobInfo(typeof(RepeatedTask))
            {
                Repeat = true,
                PeriodicTime = DateTime.Now.ToLocalTime().TimeOfDay,
                RequiredInternetAccess = InternetAccess.Any
            };
            RepeatedJob = job;
            services.RegisterJob(job);
        }
    }
}
