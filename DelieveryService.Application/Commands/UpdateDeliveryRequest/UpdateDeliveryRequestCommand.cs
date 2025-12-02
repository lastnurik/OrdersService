using MediatR;

namespace DeliveryService.Application.Commands.UpdateDeliveryRequest;

public record UpdateDeliveryRequestCommand(
    Guid Id,
    string CustomerName,
    decimal TotalAmount,
    string Description,
    DateTime CreatedAt,
    string Street,
    string City,
    string PostalCode,
    string Country,
    string? DeliveryInstructions
) : IRequest<bool>;
