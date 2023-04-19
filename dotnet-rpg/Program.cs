
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace dotnet_rpg
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var host = CreateHostBuilder(args).Build();
            CreateIfNotExists(host);
            //MigrateAysnc(host);
            host.Run();
        }

        //private static async Task MigrateAysnc(IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var loogerFactory = services.GetRequiredService<ILoggerFactory>();
        //        try
        //        {
        //            var context = services.GetRequiredService<StoreDbContext>();
        //            await context.Database.MigrateAsync();
        //            context.Database.EnsureCreated();
        //        }
        //        catch (Exception ex)
        //        {
        //            var logger = loogerFactory.CreateLogger<Program>();
        //            logger.LogError(ex, "Migrations Error");
        //        }
        //    }
        //}


        private static void CreateIfNotExists(IHost host)
        {

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseStartup<Startup>();
                });
    }
}
//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();