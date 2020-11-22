using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class UserProcessingFunctions
    {

        [FunctionName(Names.TriggerUserProcessingHttp)]
        public async Task<HttpResponseMessage> TriggerUserProcessingHttp([HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "TriggerUserProcessing/{userId}")]HttpRequestMessage request, string userId)
        {

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }
}
