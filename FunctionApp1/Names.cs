using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1
{
    public static class Names
    {

        /// <summary>
        /// The activity function that delays for a given amount of time. The input to the function must be an integer representing the number of milliseconds to delay.
        /// </summary>
        public const string Delay = nameof(Delay);

        /// <summary>
        /// The main orchestration that takes care of processing a user. The input to the function is a <see cref="string"/> representing the ID of the user to process.
        /// </summary>
        public const string ProcessUserOrchestration = nameof(ProcessUserOrchestration);

        /// <summary>
        /// The entity function that represents the state that is shared across multiple orchestration instances of <see cref="ProcessUserOrchestration"/>.
        /// </summary>
        public const string ProcessUserState = nameof(ProcessUserState);

        /// <summary>
        /// The HTTP (PATCH) triggered function that triggers a new user processing orchestration instance.
        /// </summary>
        public const string TriggerUserProcessingHttp = nameof(TriggerUserProcessingHttp);
    }
}
