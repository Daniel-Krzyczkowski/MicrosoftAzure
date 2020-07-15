using Microsoft.EntityFrameworkCore;

namespace CarsIsland.Rent.Infrastructure.Repositories
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options)
                                                      : base(options)
        {
        }
    }
}
