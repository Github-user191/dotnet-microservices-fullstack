using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using Shared.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure {
    public static class InfrastructureServiceRegistration {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
            Console.WriteLine("INJECTED IServiceCollection FROM INFRASTRUCTURE LAYER");

            services.AddDbContext<ApplicationDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"))
            );

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

        public static IApplicationBuilder AddApplicationBuilder(this IApplicationBuilder app) {
            Console.WriteLine("INJECTED IApplicationBuilder FROM INFRASTRUCTURE LAYER");

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            return app;
        }
    }
}
