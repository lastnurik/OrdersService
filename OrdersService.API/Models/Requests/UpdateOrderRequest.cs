using System.ComponentModel.DataAnnotations;

namespace OrdersService.API.Models.Requests
{
    public class UpdateOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public string? Status { get; set; }

        public string? Description { get; set; }
    }
}
