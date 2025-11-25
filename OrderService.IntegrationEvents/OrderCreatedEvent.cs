namespace OrderService.IntegrationEvents;

public record OrderCreatedEvent(
    Guid OrderId,
    string CustomerName,
    decimal TotalAmount,
    string Description,
    DateTime CreatedAt,
    string Street,
    string City,
    string PostalCode,
    string Country,
    string? DeliveryInstructions
) : IIntegrationEvent;
