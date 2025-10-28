using AutoMapper;
using OrdersService.Application.Commands.CreateOrder;
using OrdersService.Application.Commands.UpdateOrder;
using OrdersService.Application.DTOs;
using OrdersService.Domain.Entities;
using OrdersService.Domain.Enums;
namespace OrdersService.Application
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
