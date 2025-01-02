using API.Context;
using API.Exception;
using API.Interfaces;
using API.Mapper;
using API.Repositories.Implementations;
using API.Repositories.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Extension
{
    public static class DependencyExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("ProductConnection")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();


            services.AddExceptionHandler<GlobalExceptionHandler>();
        }
    }
}
