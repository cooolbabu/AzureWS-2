// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging.EventGrid;

namespace Chup.iot
{
    public static class Chup_IoT_ADT_Functions
    {
        [FunctionName("Chup_IoT_ADT_Functions")]
        public static void Run([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {
            log.LogInformation("**** Chup_AzFunc_ADT:Chup_IoT_ADT_Functions  *****");
            log.LogInformation(eventGridEvent.Data.ToString());
            log.LogInformation("**** Chup_AzFunc_ADT:Chup_IoT_ADT_Functions  *****");
        }
    }
}
