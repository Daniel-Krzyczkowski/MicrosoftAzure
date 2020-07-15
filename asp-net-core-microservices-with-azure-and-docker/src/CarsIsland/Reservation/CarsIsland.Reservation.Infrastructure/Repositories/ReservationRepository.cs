using CarsIsland.Reservation.Domain.Model;
using CarsIsland.Reservation.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsIsland.Reservation.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ILogger<ReservationRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public ReservationRepository(ILogger<ReservationRepository> logger,
                                     ConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteReservationAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerReservation> GetReservationAsync(string customerId)
        {
            var data = await _database.StringGetAsync(customerId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<CustomerReservation>(data);
        }

        public async Task<CustomerReservation> UpdateReservationAsync(CustomerReservation reservation)
        {
            var created = await _database.StringSetAsync(reservation.CustomerId.ToString(),
                                                         JsonConvert.SerializeObject(reservation));

            if (!created)
            {
                _logger.LogInformation("Problem occur persisting the reservation.");
                return null;
            }

            _logger.LogInformation("Reservation persisted succesfully.");

            return await GetReservationAsync(reservation.CustomerId.ToString());
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();

            return data?.Select(k => k.ToString());
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }
    }
}
