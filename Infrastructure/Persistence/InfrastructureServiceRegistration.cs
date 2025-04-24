using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration Configuration)
        {

            services.AddDbContext<StoreDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
             ));
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddSingleton<IConnectionMultiplexer>(
                (serviceProvider) =>
                {
                    return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis")!);
                }
                );
            //services.AddScoped<IBasketRepository, IBasketRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
