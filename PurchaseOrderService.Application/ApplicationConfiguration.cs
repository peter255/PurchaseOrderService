
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
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


            // Add FluentValidation
            services.AddControllers();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // Register Validators using Assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(ms => ms.Value.Errors.Any())
                        .Select(ms => new
                        {
                            Field = ms.Key,
                            Errors = ms.Value.Errors.Select(e => e.ErrorMessage).ToList()
                        });

                    return new BadRequestObjectResult(new { Message = "Validation failed", Errors = errors });
                };
            });

        }
    }
}
