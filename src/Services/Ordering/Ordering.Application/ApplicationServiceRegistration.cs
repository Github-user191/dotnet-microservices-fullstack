using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using Shared.Configuration;

namespace Ordering.Application {
    public static class ApplicationServiceRegistration {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            Console.WriteLine("INJECTED IServiceCollection FROM APPLICATION LAYER");
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }

        public static IApplicationBuilder AddApplicationBuilder(this IApplicationBuilder app) {
            Console.WriteLine("INJECTED IApplicationBuilder FROM APPLICATION LAYER");

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            return app;
        }

    }
}
