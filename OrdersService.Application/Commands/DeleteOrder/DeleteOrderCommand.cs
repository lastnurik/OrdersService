using MediatR;

namespace OrdersService.Application.Commands.DeleteOrder
{
    public record DeleteOrderCommand
    (
        Guid Id
        ) : IRequest<bool> ;
}
