using System;

namespace TechnicalRadiation.Models.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("You are not authorized, Access forbidden!") {}
        public UnauthorizedException(string message) : base(message)  {}
        public UnauthorizedException(string message, Exception inner) : base(message, inner) {}
    }
}