namespace CarsIsland.Reservation.Infrastructure.Configuration.Interfaces
{
    public interface IRedisConfiguration
    {
        string ConnectionString { get; set; }
    }
}
