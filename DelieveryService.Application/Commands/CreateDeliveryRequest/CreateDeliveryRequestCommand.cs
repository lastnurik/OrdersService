using MediatR;

namespace DeliveryService.Application.Commands.CreateDeliveryRequest;

public record CreateDeliveryRequestCommand(
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
) : IRequest<Guid>;
