using AutoMapper;
using DeliveryService.Application.Commands.CreateDeliveryRequest;
using DeliveryService.Application.DTOs;
using DeliveryService.Domain.Entities;
using OrderService.IntegrationEvents;

namespace DeliveryService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DeliveryRequest, DeliveryRequestDto>();

        CreateMap<OrderCreatedEvent, DeliveryRequest>();
        CreateMap<OrderCreatedEvent, CreateDeliveryRequestCommand>();
        CreateMap<CreateDeliveryRequestCommand, DeliveryRequest>();
    }
}
