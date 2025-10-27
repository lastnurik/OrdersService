using MediatR;

namespace OrdersService.Application.Orders.Commands
{
    public record CreateOrderCommand
    (
        string CustomerName,
        decimal TotalAmount,
        string Description
        ) : IRequest<Guid> ;
}
