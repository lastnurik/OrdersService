using Microsoft.EntityFrameworkCore;
using MassTransit;
using DeliveryService.Application;
using DeliveryService.Application.Consumers;
using DeliveryService.Application.Commands.CreateDeliveryRequest;
using DeliveryService.Domain.Repositories;
using DeliveryService.Infrastructure.Data;
using DeliveryService.Infrastructure.Repositories;

namespace DeliveryService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDeliveryServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DeliveryDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            services.AddScoped<IDeliveryRequestRepository, DeliveryRequestRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDeliveryRequestCommand).Assembly));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMassTransit(cfg =>
            {
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.AddConsumer<OrderCreatedConsumer>();
                cfg.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(configuration["RabbitMQ:Host"] ?? "localhost", h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"] ?? "guest");
                        h.Password(configuration["RabbitMQ:Password"] ?? "guest");
                    });
                    bus.ReceiveEndpoint("delivery-service-order-created", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedConsumer>(context);
                        e.PrefetchCount = 16;
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                    });
                });
            });

            return services;
        }
    }
}
