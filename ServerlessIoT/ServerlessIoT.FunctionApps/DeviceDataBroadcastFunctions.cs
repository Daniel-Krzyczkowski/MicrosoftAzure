using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace ServerlessIoT.FunctionApps
{
    public static class DeviceDataBroadcastFunctions
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
           [SignalRConnectionInfo(HubName = "devicedata")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("messages")]
        public static Task SendMessage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] string message,
            [SignalR(HubName = "devicedata")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[] { message }
                });
        }
    }
}
