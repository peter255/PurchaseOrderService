using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurchaseOrderService.Domain.Interfaces;
using PurchaseOrderService.Infrastructure.Persistence;
using PurchaseOrderService.Infrastructure.Repositories;

namespace PurchaseOrderService.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static void AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PurchaseOrderDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PurchaseOrderConnectionString"));
            });
 
            services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
        }
    }
}
