using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureSignalRTransportApp.WebAPI.Config;
using AzureSignalRTransportApp.WebAPI.Model;
using AzureSignalRTransportApp.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureSignalRTransportApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class MapController : Controller
    {
        private IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DirectionsRequest directionsRequest)
        {
            var directions = await _mapService.GetDirections(directionsRequest);
            if (directions == null)
                return NotFound();
            return Ok(directions);
        }
    }
}
