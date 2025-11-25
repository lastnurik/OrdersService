using MediatR;

namespace OrdersService.Application.Commands.CreateOrder
{
    public record CreateOrderCommand
    (
        string CustomerName,
        decimal TotalAmount,
        string Description,
        string Street,
        string City,
        string PostalCode,
        string Country,
        string? DeliveryInstructions
    ) : IRequest<Guid> ;
}
