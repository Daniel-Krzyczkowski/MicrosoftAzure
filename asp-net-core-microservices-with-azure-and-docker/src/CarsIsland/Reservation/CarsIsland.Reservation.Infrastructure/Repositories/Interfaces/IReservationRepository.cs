using CarsIsland.Reservation.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.Reservation.Domain.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<CustomerReservation> GetReservationAsync(string customerId);
        Task<CustomerReservation> UpdateReservationAsync(CustomerReservation reservation);
        Task<bool> DeleteReservationAsync(string id);
        IEnumerable<string> GetUsers();
    }
}
