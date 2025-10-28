using OrdersService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Application.DTOs
{
    public record OrderDto
    (
        Guid Id,
        string CustomerName,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        decimal TotalAmount,
        OrderStatus Status,
        string Description
    );
}
