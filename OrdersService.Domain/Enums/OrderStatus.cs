using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 0,
        Shipped = 1,
        Delievered = 2,
        Cancelled = 3,
    }
}
