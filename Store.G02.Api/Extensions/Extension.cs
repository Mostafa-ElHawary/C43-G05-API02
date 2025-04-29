using System.Text;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Identity;
using Services;
using Shared;
using Shared.ErrorModels;
using Store.G02.Api.MiddleWares;

namespace Store.G02.Api.Extensions
{
    public static class Extension
    {
        public static IServiceCollection RegisterAllApplicationService(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.ConfigurServices();
            services.AddInfrastructureServices(Configuration);
            services.AddIdentityServices();
            services.AddApplicationServices(Configuration);
            services.ConfigureJwtServices(Configuration);




            return services;
        }

        private static IServiceCollection AddBuiltInServices( this IServiceCollection services)
        {

            services.AddControllers();
            return services;

        }

        private static IServiceCollection ConfigureJwtServices(this IServiceCollection services , IConfiguration Configuration)
        {

            var jwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    // validation
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
            return services;

        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();
               
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();


            return app;

        }

        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeAsync();

            await dbInitializer.InitializeIdentityAsync();
            return app;
        }

        private static  WebApplication UseGlobalExcpetionMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();

            return app;
        }

    }

}
