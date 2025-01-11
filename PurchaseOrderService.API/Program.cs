using PurchaseOrderService.Infrastructure;
using PurchaseOrderService.Application;
using Serilog;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfrastructureConfiguration(builder.Configuration);
builder.Services.AddApplicationConfiguration();


builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Purchase Order Service";
});

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Logs to console
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Logs to a file
    .CreateLogger();

builder.Host.UseSerilog((context, configuration) =>
{
    var elasticUri = context.Configuration["ElasticConfiguration:Uri"];
    var elasticUsername = context.Configuration["ElasticConfiguration:Username"];
    var elasticPassword = context.Configuration["ElasticConfiguration:Password"];

    configuration
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
        {
            AutoRegisterTemplate = true,
            IndexFormat = "po-sho-logs-{0:yyyy.MM.dd}",
            ModifyConnectionSettings = conn =>
                conn.BasicAuthentication(elasticUsername, elasticPassword)
        });
});



var app = builder.Build();

app.UseSerilogRequestLogging(); // Add Serilog middleware for request logging

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
