using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement_MinhNTSE196686.Filter;

namespace RealEstateManagement_MinhNTSE196686
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<Fa25realEstateDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext")));
            builder.Services.AddScoped(typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(GenericService<>));

            builder.Services.AddSession();

            builder.Services.AddSignalR();

            builder.Services.AddScoped<RoleAuthorizationFilter>();
            builder.Services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<RoleAuthorizationFilter>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.MapHub<RealEstateManagement_MinhNTSE196686.Hubs.SignalRServer>("/signalRServer");

            app.Run();
        }
    }
}
