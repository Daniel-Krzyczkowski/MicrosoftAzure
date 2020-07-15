using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarsIsland.Rent.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase
    {
        private readonly ILogger<RentController> _logger;

        public RentController(ILogger<RentController> logger)
        {
            _logger = logger;
        }
    }
}
