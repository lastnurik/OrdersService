using MediatR;
using OrdersService.Domain.Enums;

namespace OrdersService.Application.Commands.UpdateOrder
{
    public record UpdateOrderCommand
    (
        Guid Id,
        string CustomerName,
        decimal TotalAmount,
        OrderStatus Status,
        string Description
        ) : IRequest<bool>;
}
