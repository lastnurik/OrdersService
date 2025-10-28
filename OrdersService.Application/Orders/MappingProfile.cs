using AutoMapper;
using OrdersService.Application.DTOs;
using OrdersService.Application.Orders.Commands;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Enums;
namespace OrdersService.Application.Orders
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderCommand, Order>();
            CreateMap<UpdateOrderCommand, Order>();
            CreateMap<Order, OrderDto>();
        }
    }
}
