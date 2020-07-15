using CarsIsland.Reservation.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace CarsIsland.Reservation.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context;

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Guid GetUserIdentity()
        {
            var userId = _context.HttpContext.User.FindFirst("sub").Value;
            return Guid.Parse(userId);
        }
    }
}
