
using AutoMapper;
using E_Commerce.Web.Extentions;
using ECommerce.Domain.Contracts;
using ECommerce.Persistance.Data.DataSeed;
using ECommerce.Persistance.DbContexts;
using ECommerce.Persistance.Repositories;
using ECommerce.Services.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataInitializer, DataIntializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(X => X.AddProfile<ProductProfile>());




            #endregion

            var app = builder.Build();
            await app.MigrateDataBaseAsync();
            await app.SeedDataAsync();

            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            #endregion

            await app.RunAsync();
        }
    }
}
