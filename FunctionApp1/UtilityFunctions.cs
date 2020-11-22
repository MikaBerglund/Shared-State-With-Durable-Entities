using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class UtilityFunctions
    {

        [FunctionName(Names.Delay)]
        public async Task Delay([ActivityTrigger]IDurableActivityContext context)
        {
            var ms = context.GetInput<int>();
            await Task.Delay(ms);
        }
    }
}
