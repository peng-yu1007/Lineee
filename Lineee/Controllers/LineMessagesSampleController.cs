using Newtonsoft.Json;
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


            return Request.CreateResponse(HttpStatusCode.OK, "Hello LINE BOT");



        }
    }
}