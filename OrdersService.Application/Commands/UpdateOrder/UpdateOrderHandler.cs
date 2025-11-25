using AutoMapper;
using MediatR;
using OrdersService.Domain.Repositories;
using System;

namespace OrdersService.Application.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, bool>
    {
        private readonly IOrderRepository _repo;

        public UpdateOrderHandler(IOrderRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _repo.GetByIdAsync(request.Id, cancellationToken);

            if (existingEntity == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(request.CustomerName))
            {
                existingEntity.CustomerName = request.CustomerName;
            }

            existingEntity.TotalAmount = request.TotalAmount;
            existingEntity.Status = request.Status;
            if (!string.IsNullOrEmpty(request.Description))
            {
                existingEntity.Description = request.Description;
            }

            // Update address fields
            if (!string.IsNullOrEmpty(request.Street))
                existingEntity.Street = request.Street;
            if (!string.IsNullOrEmpty(request.City))
                existingEntity.City = request.City;
            if (!string.IsNullOrEmpty(request.PostalCode))
                existingEntity.PostalCode = request.PostalCode;
            if (!string.IsNullOrEmpty(request.Country))
                existingEntity.Country = request.Country;
            existingEntity.DeliveryInstructions = request.DeliveryInstructions;

            existingEntity.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existingEntity, cancellationToken);
            return true;
        }
    }
}
