
using Microsoft.Extensions.DependencyInjection;
using PurchaseOrderService.Application.Commands.CreatePurchaseOrder;
using PurchaseOrderService.Application.Commands.DeactivatePurchaseOrder;
namespace PurchaseOrderService.Application
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePurchaseOrderCommand).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeactivatePurchaseOrderCommand).Assembly));
        }
    }
}
