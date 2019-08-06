using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace ServerlessIoT.FunctionApps
{
    public static class DeviceDataTriggerFunction
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("device-data-trigger")]
        public static async Task Run([IoTHubTrigger("messages/events", Connection = "IoTHubConnectionString")]EventData message, ILogger log)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body.Array);
            log.LogInformation($"C# IoT Hub trigger function processed a message: {messageBody}");

            HttpContent messageContent = new StringContent(messageBody, Encoding.UTF8, "application/json");
            var broadcastFunctionUrl = Environment.GetEnvironmentVariable("DeviceDataTriggerFunctionUrl", EnvironmentVariableTarget.Process);
            await client.PostAsync(broadcastFunctionUrl, messageContent);
        }
    }
}