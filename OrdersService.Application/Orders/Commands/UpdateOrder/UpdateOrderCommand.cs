using MediatR;
using OrdersService.Domain.Enums;

namespace OrdersService.Application.Orders.Commands
{
    public record UpdateOrderCommand
    (
        Guid Id,
        string CustomerName,
        decimal TotalAmount,
        string Status,
        string Description
        ) : IRequest<bool>;
}
