using System;

namespace TechnicalRadiation.Models.Exceptions
{
    public class ModelFormatException : Exception
    {
        public ModelFormatException() : base("The model is not formatted properly") {}
        public ModelFormatException(string message) : base(message)  {}
        public ModelFormatException(string message, Exception inner) : base(message, inner) {}
    }
}