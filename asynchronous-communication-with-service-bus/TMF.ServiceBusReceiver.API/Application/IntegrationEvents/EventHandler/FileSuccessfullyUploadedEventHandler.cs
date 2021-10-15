using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TMF.ServiceBusReceiver.Common;

namespace TMF.ServiceBusReceiver.API.Application.IntegrationEvents.EventHandlers
{
    internal class FileSuccessfullyUploadedEventHandler : IIntegrationEventHandler<FileSuccessfullyUploadedIntegrationEvent>
    {
        private readonly ILogger<FileSuccessfullyUploadedEventHandler> _logger;

        public FileSuccessfullyUploadedEventHandler(ILogger<FileSuccessfullyUploadedEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(FileSuccessfullyUploadedIntegrationEvent @event)
        {
            if (!string.IsNullOrEmpty(@event.FileUrl)
                                            && !string.IsNullOrEmpty(@event.UserId))
            {
                _logger.LogInformation($"Received new event with user ID: {@event.UserId} and file URL: {@event.FileUrl}");
            }
        }
    }
}
