using MassTransit;
using OrdersService.Application.IntegrationEvents;

namespace Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderCreatedConsumer> _logger;

        public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var msg = context.Message;
            _logger.LogInformation("Consumed OrderCreatedEvent: Id={Id}, Customer={Customer}, Total={Total}, CreatedAt={CreatedAt}",
                msg.Id, msg.CustomerName, msg.TotalAmount, msg.CreatedAt);

            return Task.CompletedTask;
        }
    }
}
