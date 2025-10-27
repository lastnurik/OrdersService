using MediatR;

namespace OrdersService.Application.Orders.Commands
{
    public record DeleteOrderCommand
    (
        Guid Id
        ) : IRequest<bool> ;
}
