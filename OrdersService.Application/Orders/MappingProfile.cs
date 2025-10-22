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
            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status)));
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
