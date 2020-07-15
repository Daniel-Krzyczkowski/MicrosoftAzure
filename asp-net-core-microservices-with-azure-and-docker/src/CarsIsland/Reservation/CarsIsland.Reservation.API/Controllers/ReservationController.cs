using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarsIsland.Reservation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }
    }
}
