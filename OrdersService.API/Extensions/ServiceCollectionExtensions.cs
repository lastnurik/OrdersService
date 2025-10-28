using Microsoft.EntityFrameworkCore;
using OrdersService.Domain.Repositories;
using OrdersService.Infrastructure.Data;
using OrdersService.Infrastructure.Repositories;
using OrdersService.Application.Commands.CreateOrder;
using OrdersService.Application;

namespace OrdersService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrdersService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
