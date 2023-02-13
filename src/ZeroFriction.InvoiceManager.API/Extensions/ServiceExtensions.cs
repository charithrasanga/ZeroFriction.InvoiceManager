using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZeroFriction.InvoiceManager.Infrastructure.Persistence;

namespace ZeroFriction.InvoiceManager.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseCosmos(configuration.GetSection("CosmosDBConnectionString").Value,
                databaseName: "ZeroFrictionInvoiceDB");
            });

    }

}
