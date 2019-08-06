using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.EventHubs;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ServerlessIoT.FunctionApps
{
    public static class DeviceDataTriggerFunction
    {
        private static HttpClient client = new HttpClient();

        [FunctionName("DeviceDataTrigger")]
        public static async Task Run([IoTHubTrigger("messages/events", Connection = "IoTHubConnectionString")]EventData message, ILogger log)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body.Array);
            log.LogInformation($"C# IoT Hub trigger function processed a message: {messageBody}");

            HttpContent messageContent = new StringContent(messageBody, Encoding.UTF8, "application/json");
            await client.PostAsync("http://localhost:7071/api/messages", messageContent);
        }
    }
}