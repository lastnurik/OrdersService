
using OrdersService.Domain.Enums;

namespace OrdersService.Application.IntegrationEvents
{
    public record OrderCreatedEvent(
        Guid Id,
        string CustomerName,
        DateTime CreatedAt,
        decimal TotalAmount,
        string Description
    );
}
