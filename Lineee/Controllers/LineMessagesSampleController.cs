using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lineee.Controllers
{
    [RoutePrefix("line")]
    public class LineMessagesSampleController : ApiController
    {
        [HttpGet]
        [Route]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            var content = await request.Content.ReadAsStringAsync();
            Activity activity = JsonConvert.DeserializeObject<Activity>(content);
            var client = new RestClient("https://script.google.com/macros/s/AKfycbyB2u5E72rhN3YcBzjDravC7wgMp1M-DK1ZYpoIkt10jAKafj-rZ-t7tAB8TXsr4TM/exec");
            client.Timeout = 5000;
            var request1 = new RestRequest(Method.POST);
            request1.AlwaysMultipartFormData = true;
            request1.AddParameter("msg", "123");
            IRestResponse response = client.Execute(request1);
            return Request.CreateResponse(HttpStatusCode.OK, "Hello LINE BOT");

        }
    }
}