# Use official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution file
COPY PurchaseOrderService.sln ./

# Copy all project files
COPY PurchaseOrderService.API/ PurchaseOrderService.API/
COPY PurchaseOrderService.Application/ PurchaseOrderService.Application/
COPY PurchaseOrderService.Domain/ PurchaseOrderService.Domain/
COPY PurchaseOrderService.Infrastructure/ PurchaseOrderService.Infrastructure/
COPY PurchaseOrderService.Tests/ PurchaseOrderService.Tests/

# Restore dependencies
RUN dotnet restore PurchaseOrderService.sln

# Build the application
RUN dotnet publish PurchaseOrderService.API/PurchaseOrderService.API.csproj -c Release -o out

# Use runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose the port
EXPOSE 5000

# Run the application
ENTRYPOINT ["dotnet", "PurchaseOrderService.API.dll"]
