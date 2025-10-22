

namespace OrdersService.Application.Orders.Queries
{
    public record GetAllOrdersQuery
    (
        int PageNumber = 1,
        int PageSize = 10
        );
}
