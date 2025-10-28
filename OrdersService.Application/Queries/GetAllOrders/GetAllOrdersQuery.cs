using MediatR;
using OrdersService.Application.DTOs;

namespace OrdersService.Application.Queries.GetAllOrders
{
    public record GetAllOrdersQuery
    (
        int PageNumber = 1,
        int PageSize = 10
        ) : IRequest<PaginatedResult<OrderDto>> ;
}
