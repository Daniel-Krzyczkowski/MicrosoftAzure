using System;

namespace CarsIsland.Reservation.API.Services.Interfaces
{
    internal interface IIdentityService
    {
        Guid GetUserIdentity();
    }
}
