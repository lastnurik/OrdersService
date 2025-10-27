using MediatR;
using OrdersService.Application.DTOs;

namespace OrdersService.Application.Orders.Queries
{
    public record GetOrderByIdQuery
    (
        Guid Id
        ) : IRequest<OrderDto?> ;
}
