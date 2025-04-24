using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ErrorModels;
using Store.G02.Api.MiddleWares;

namespace Store.G02.Api.Extensions
{
    public static class Extension
    {
        public static IServiceCollection RegisterAllApplicationService(this IServiceCollection services, IConfiguration Configuration)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.ConfigurServices();
            services.AddInfrastructureServices(Configuration);
            services.AddApplicationServices();

            return services;
        }

        private static IServiceCollection AddBuiltInServices( this IServiceCollection services)
        {

            services.AddControllers();
            return services;

        }

        private static IServiceCollection AddSwaggerServices( this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;

        }

        private static IServiceCollection ConfigurServices( this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(config =>

            config.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Any())
                    .Select(x => new ValidationErrors()
                    {
                        Field = x.Key,
                        Errors = x.Value.Errors.Select(e => e.ErrorMessage)

                    });
                //.Select(x => x.ErrorMessage).ToArray();
                var errorResponse = new ValidationErrorResponse()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            }

            );


            return services;

        }


        public static async Task<WebApplication> ConfigureMiddleWares( this WebApplication app)
        {


            #region Seeding

       await   app.InitializeDatabaseAsync();

            #endregion

            app.UseGlobalExcpetionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            return app;

        }

        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();
            return app;
        }

        private static  WebApplication UseGlobalExcpetionMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();

            return app;
        }

    }

}
