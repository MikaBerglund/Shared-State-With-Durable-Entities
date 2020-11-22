using FunctionApp1.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
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
        public async Task<HttpResponseMessage> TriggerUserProcessingHttp([HttpTrigger(AuthorizationLevel.Function, "PATCH", Route = "TriggerUserProcessing/{userId}")]HttpRequestMessage request, string userId, [DurableClient]IDurableClient durableClient)
        {
            var instanceId = await durableClient.StartNewAsync<string>(Names.ProcessUserOrchestration, userId);
            return durableClient.CreateCheckStatusResponse(request, instanceId);
        }



        [FunctionName(Names.ProcessUserOrchestration)]
        public async Task ProcessUserOrchestration([OrchestrationTrigger]IDurableOrchestrationContext context, ILogger logger)
        {
            //------------------------------------------------------------------------------------------------------------------------------------
            // Get the user ID that will be processed in the current orchestration.
            var userId = context.GetInput<string>();
            if (string.IsNullOrEmpty(userId))
            {
                logger.LogError($"No user ID was passed as input to the '{context.Name}' orchestration running with instance ID '{context.InstanceId}'. The orchestration cannot continue and will now exit.");
                return;
            }
            //------------------------------------------------------------------------------------------------------------------------------------



            //------------------------------------------------------------------------------------------------------------------------------------
            // Create a reference to the entity, and initialize the entity with the orchestration ID for the current instance.
            // This will make the current orchestration the current for handling processing of a given user, invalidating any other
            // running instance that has been started for the same user.
            var eid = new EntityId(Names.ProcessUserState, userId);
            await context.CallEntityAsync(eid, nameof(ProcessUserState.InitializeUserProcessing) , context.InstanceId);
            //------------------------------------------------------------------------------------------------------------------------------------



            //------------------------------------------------------------------------------------------------------------------------------------
            // Simulating some work performed in the orchestration to allow other orchestrations to be triggered while this is still running.
            await context.CallActivityAsync(Names.Delay, 5000);
            //------------------------------------------------------------------------------------------------------------------------------------



            //------------------------------------------------------------------------------------------------------------------------------------
            // After we've done some processing, we check if the currently running orchestration is still the current orchestration for
            // the user that the orchestration is processing. We then make decisions based on the information returned from the entity.

            // We can repeat this process as many times we need in the orchestration, for instance if processing involves several steps that
            // call into sub orchestrations or activity functions. Whatever makes sense for your particular case.
            var isCurrent = await context.CallEntityAsync<bool>(eid, nameof(ProcessUserState.IsCurrentOrchestration), context.InstanceId);
            if(isCurrent)
            {
                logger.LogInformation($"The orchestration '{context.Name}' with instance '{context.InstanceId}' is still the current orchestration and will continue running.");
            }
            else
            {
                logger.LogWarning($"A newer orchestration instance of the orchestation '{context.Name}' for user ID '{userId}' has been started. This instance ('{context.InstanceId}') should not perform any processing anymore.");
            }
            //------------------------------------------------------------------------------------------------------------------------------------
        }
    }
}
