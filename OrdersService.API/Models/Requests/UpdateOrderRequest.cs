using OrdersService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrdersService.API.Models.Requests
{
    public class UpdateOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string Country { get; set; } = string.Empty;
        public string? DeliveryInstructions { get; set; }
    }
}
