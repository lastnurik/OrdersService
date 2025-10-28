using MediatR;

namespace OrdersService.Application.Commands.CreateOrder
{
    public record CreateOrderCommand
    (
        string CustomerName,
        decimal TotalAmount,
        string Description
        ) : IRequest<Guid> ;
}
