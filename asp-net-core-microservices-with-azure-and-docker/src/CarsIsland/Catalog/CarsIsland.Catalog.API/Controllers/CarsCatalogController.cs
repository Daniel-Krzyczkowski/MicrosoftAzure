using CarsIsland.Catalog.API.Core.IntegrationEvents.Events;
using CarsIsland.Catalog.Domain.Model;
using CarsIsland.Catalog.Infrastructure.Repositories;
using CarsIsland.Catalog.Infrastructure.Services.Integration.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CarsIsland.Catalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsCatalogController : ControllerBase
    {
        private readonly CarCatalogDbContext _carCatalogDbContext;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public CarsCatalogController(CarCatalogDbContext carCatalogDbContext,
                                     ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            _carCatalogDbContext = carCatalogDbContext;
            _catalogIntegrationEventService = catalogIntegrationEventService;
        }

        /// <summary>
        /// Gets list with available cars in the catalog
        /// </summary>
        [ProducesResponseType(typeof(IReadOnlyList<Car>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllCarsAsync()
        {
            var cars = await _carCatalogDbContext.Cars.ToListAsync();
            return Ok(cars);
        }

        /// <summary>
        /// Gets car from the catalog
        /// </summary>
        [ProducesResponseType(typeof(Car), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarAsync(Guid id)
        {
            var car = await _carCatalogDbContext.Cars.SingleOrDefaultAsync(i => i.Id == id);
            if (car == null)
            {
                return NotFound(new { Message = $"Car with id {id} not found." });
            }
            return Ok(car);
        }

        /// <summary>
        /// Add new car to the catalog
        /// </summary>
        [ProducesResponseType(typeof(Car), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddCarAsync([FromBody] Car car)
        {
            var addedCar = _carCatalogDbContext.Add(car);
            await _carCatalogDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCarAsync), new { id = addedCar.Entity.Id });
        }


        /// <summary>
        /// Update existing car in the catalog
        /// </summary>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> UpdateCarAsync([FromBody] Car carToUpdate)
        {
            var existingCarFromTheCatalog = await _carCatalogDbContext.Cars.SingleOrDefaultAsync(i => i.Id == carToUpdate.Id);

            if (existingCarFromTheCatalog == null)
            {
                return NotFound(new { Message = $"Car with id {carToUpdate.Id} not found." });
            }

            else
            {
                var oldPricePerDay = existingCarFromTheCatalog.PricePerDay;
                var hasPricePerDayChanged = existingCarFromTheCatalog.PricePerDay != carToUpdate.PricePerDay;
                existingCarFromTheCatalog.PricePerDay = carToUpdate.PricePerDay;

                _carCatalogDbContext.Cars.Update(existingCarFromTheCatalog);

                if (hasPricePerDayChanged)
                {
                    var pricePerDayChangedEvent = new CarPricePerDayChangedIntegrationEvent(existingCarFromTheCatalog.Id,
                                                                                            existingCarFromTheCatalog.PricePerDay,
                                                                                            oldPricePerDay);

                    await _catalogIntegrationEventService.AddAndSaveEventAsync(pricePerDayChangedEvent);
                    await _catalogIntegrationEventService.PublishEventsThroughEventBusAsync(pricePerDayChangedEvent);
                }

                else
                {
                    await _carCatalogDbContext.SaveChangesAsync();
                }
                return NoContent();
            }
        }
    }
}
