using RestSharp;
using Shiny.Jobs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundJob
{
    public class RepeatedTask : IJob
    {
        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            var client = new RestClient("https://kidsandfun.site/wp-admin/admin-ajax.php")
            {
                Timeout = -1
            };
            
            var request = new RestRequest(Method.POST);
            request.AddHeader("User-Agent", "Mozilla/4.0 (Macintosh; Intel Mac OS X 10.13; rv:71.0) Gecko/20100101 Firefox/"+ jobInfo.PeriodicTime?.Seconds);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Accept-Language", "en-US,en;q=0.5");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.AddHeader("X-Requested-With", "XMLHttpRequest");
            request.AddHeader("Origin", "https://kidsandfun.site");
            request.AddHeader("DNT", "1");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("Referer", "https://kidsandfun.site/february-2020/?contest=photo-detail&photo_id=1119");
            request.AddHeader("TE", "Trailers");
            request.AddHeader("Cookie", "4");
            request.AddParameter("application/x-www-form-urlencoded; charset=UTF-8", "action=vote_for_photo&photo_id=1119&nonce_id=02802094c7&option_id=basicaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",  ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine("response :-"+response.Content);


            return true;
        }
    }
}
