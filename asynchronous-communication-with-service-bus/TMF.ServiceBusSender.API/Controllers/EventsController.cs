using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TMF.ServiceBusReceiver.Common;
using TMF.ServiceBusSender.API.Application.IntegrationEvents;

namespace TMF.ServiceBusSender.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {

        private readonly ILogger<EventsController> _logger;
        private readonly IEventBus _eventBus;

        public EventsController(ILogger<EventsController> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpPost]
        public async Task<IActionResult> PublishEvent()
        {
            var fileSuccessfullyUploadedIntegrationEvent = new FileSuccessfullyUploadedIntegrationEvent()
            {
                CreationDate = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                FileUrl = "https://this-is-uploaded-file-url-value",
                UserId = Guid.NewGuid().ToString()
            };
            await _eventBus.PublishAsync(fileSuccessfullyUploadedIntegrationEvent);
            return Ok();
        }
    }
}
