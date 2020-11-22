using FunctionApp1.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class EntityFunctions
    {

        [FunctionName(Names.ProcessUserState)]
        public Task ProcessUserState([EntityTrigger]IDurableEntityContext context)
        {
            return context.DispatchAsync<ProcessUserState>();
        }
    }
}
