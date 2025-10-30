
using Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Contracts;
using Persistance.Data.DataSeed;
using Persistance.Repositories;
using AutoMapper;
using Services;
using Services.Abstractions;
using E_Commerce.CustomMiddleware;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using E_Commerce.Factories;
using E_Commerce.Extentions;
using StackExchange.Redis;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //----------------------------------------------------------------------------------------
            #region Configure Services
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddCoreServices();
            builder.Services.AddTransient<PictureUrlResolver>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });

            builder.Services.Configure<ApiBehaviorOptions> ((options) =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateApiValidationErrorResponse;
            });
            #endregion
            //------------------------------------------------------------------------------------
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

           
            var app = builder.Build();
            //-----------------
            #region Services
            var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>(); 
            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.IdentityDataSeed();
            #endregion
            //-----------------

            // Configure the HTTP request pipeline.
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); //3lashan ne3raf nesha8al el images

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
