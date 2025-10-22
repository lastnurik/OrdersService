using System;

namespace OrdersService.Application.Exceptions
{
    public class InvalidOrderStatusException : Exception
    {
        public string InvalidValue { get; }

        public InvalidOrderStatusException(string value)
            : base($"Invalid order status value: '{value}'.")
        {
            InvalidValue = value;
        }
    }
}
