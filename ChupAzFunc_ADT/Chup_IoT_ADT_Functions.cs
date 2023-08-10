// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using Azure;
using Azure.Core.Pipeline;
using Azure.DigitalTwins.Core;
using Azure.Identity;

using Azure.Messaging.EventGrid;
//using Microsoft.Azure.EventGrid.Models; // Why is Models better

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

using System.Threading.Tasks;

namespace Chup.iot
{
    public static class Chup_IoT_ADT_Functions
    {

        private static string show_event_details = Environment.GetEnvironmentVariable("SHOW_EVENT_DETAILS");
        private static readonly string adtInstanceUrl = Environment.GetEnvironmentVariable("ADT_SERVICE_URL");
        private static readonly HttpClient httpClient = new HttpClient();

        [FunctionName("Chup_IoT_ADT_Functions")]
        public static async Task RunAsync([EventGridTrigger] EventGridEvent eventGridEvent, ILogger log)
        {

            log.LogInformation(eventGridEvent.Data.ToString());

            if (String.Equals(show_event_details, "true", StringComparison.OrdinalIgnoreCase))
                ShowEventGridEventDetails(eventGridEvent, log);

            if (adtInstanceUrl == null) log.LogError("Application setting \"ADT_SERVICE_URL\" not set");

            try
            {
                // Authenticate with Digital Twins
                var cred = new DefaultAzureCredential();
                var client = new DigitalTwinsClient(
                    new Uri(adtInstanceUrl),
                    cred,
                    new DigitalTwinsClientOptions { Transport = new HttpClientTransport(httpClient) });
                log.LogInformation($"ADT service client connection created.");

                if (eventGridEvent != null && eventGridEvent.Data != null)
                {
                    log.LogInformation(eventGridEvent.Data.ToString());

                    // <Find_device_ID_and_temperature>
                    JObject deviceMessage = (JObject)JsonConvert.DeserializeObject(eventGridEvent.Data.ToString());
                    string deviceId = (string)deviceMessage["systemProperties"]["iothub-connection-device-id"];
                    var temperature = deviceMessage["body"]["Temperature"];

                    log.LogInformation($"Device:{deviceId} Temperature is:{temperature.Value<double>()}");


                    // <Update_twin_with_device_temperature>
                    var updateTwinData = new JsonPatchDocument();
                    updateTwinData.AppendReplace("/Temperature", temperature.Value<double>());
                    //updateTwinData.AppendReplace("/temperatureAlert", machineAlert.Value<bool>());
                    await client.UpdateDigitalTwinAsync(deviceId, updateTwinData);


                }
            }
            catch (Exception ex)
            {
                log.LogError($"Error in ingest function: {ex.Message}");
            }

        }

        private static void ShowEventGridEventDetails(EventGridEvent egEvent, ILogger log)
        {

            log.LogInformation("**** Chup_AzFunc_ADT:Chup_IoT_ADT_Functions  *****");
            log.LogInformation($"EventId: {egEvent.Id}, Subject: {egEvent.Subject}");
            log.LogInformation($"Event type: {egEvent.EventType}, Topic: {egEvent.Topic}, DataVersion: {egEvent.DataVersion}");
            log.LogInformation(egEvent.Data.ToString());
            log.LogInformation("**** Chup_AzFunc_ADT:Chup_IoT_ADT_Functions  *****");

        }
    }
}
