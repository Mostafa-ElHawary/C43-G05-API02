using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Services
{
    public static class ApplicationServicsRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(AssemblyReference).Assembly);

            return services;
        }
    }
}
