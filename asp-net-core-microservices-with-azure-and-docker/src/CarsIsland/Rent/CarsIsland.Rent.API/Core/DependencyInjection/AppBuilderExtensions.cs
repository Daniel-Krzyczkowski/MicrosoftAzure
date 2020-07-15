using Microsoft.AspNetCore.Builder;

namespace CarsIsland.Rent.API.Core.DependencyInjection
{
    public static class AppBuilderExtensions
    {
        public static void UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cars Island Rent API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
