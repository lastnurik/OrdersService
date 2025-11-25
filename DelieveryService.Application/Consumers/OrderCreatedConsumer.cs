using AutoMapper;
using DeliveryService.Application.Commands.CreateDeliveryRequest;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderService.IntegrationEvents;

namespace DeliveryService.Application.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderCreatedConsumer> _logger;
    private readonly IMapper _mapper;

    public OrderCreatedConsumer(IMediator mediator, ILogger<OrderCreatedConsumer> logger, IMapper mapper)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var m = context.Message;
        _logger.LogInformation("OrderCreated received for OrderId {OrderId}", m.OrderId);

        var cmd = _mapper.Map<CreateDeliveryRequestCommand>(m);

        await _mediator.Send(cmd, context.CancellationToken);
        _logger.LogInformation("DeliveryRequest created for OrderId {OrderId}", cmd.OrderId);
    }
}
