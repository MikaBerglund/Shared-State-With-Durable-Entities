using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1.Entities
{
    /// <summary>
    /// A simple class used as durable entity.
    /// </summary>
    /// <remarks>
    /// The ID of the user that this entity represents is the key to the entity, and is not part of the entity class itself.
    /// </remarks>
    public class ProcessUserState
    {
        /// <summary>
        /// The ID of the orchestration that represents the current orchestration handling user processing for a given user.
        /// </summary>
        public string CurrentOrchestrationId { get; set; }


        /// <summary>
        /// Initializes the user processing state by making <paramref name="orchestrationId"/> the current orchestration.
        /// </summary>
        /// <param name="orchestrationId"></param>
        public void InitializeUserProcessing(string orchestrationId)
        {
            this.CurrentOrchestrationId = orchestrationId;
        }

        /// <summary>
        /// Returns <c>true</c> if <paramref name="orchestrationId"/> matches the value stored in <see cref="CurrentOrchestrationId"/>.
        /// </summary>
        /// <param name="orchestrationId">The ID of the orchestration to check.</param>
        /// <returns></returns>
        public bool IsCurrentOrchestration(string orchestrationId)
        {
            return orchestrationId == this.CurrentOrchestrationId;
        }
    }

}
