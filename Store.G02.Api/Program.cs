
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstractions;
using Shared.ErrorModels;
using Store.G02.Api.Extensions;
using Store.G02.Api.MiddleWares;

//using Services;
using AssemblyMapping = Services.AssemblyReference;
namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.RegisterAllApplicationService(builder.Configuration);


            var app = builder.Build();


            await app.ConfigureMiddleWares();
            app.Run();
        }


    }
}
