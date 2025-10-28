using MediatR;
using OrdersService.Application.DTOs;

namespace OrdersService.Application.Queries.GetOrderById
{
    public record GetOrderByIdQuery
    (
        Guid Id
        ) : IRequest<OrderDto?> ;
}
