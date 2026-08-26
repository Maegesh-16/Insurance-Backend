using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Contracts;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Services;

namespace PaymentService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPaymentInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PaymentDb")
            ?? throw new InvalidOperationException("Connection string 'PaymentDb' was not found.");

        services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IPaymentManagementService, PaymentManagementService>();

        return services;
    }
}
