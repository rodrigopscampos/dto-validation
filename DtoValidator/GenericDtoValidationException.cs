using System;

namespace DtoValidation
{

    [Serializable]
    public abstract class GenericDtoValidationException : Exception
    {
        public GenericDtoValidationException() { }
        public GenericDtoValidationException(string message) : base(message) { }
        public GenericDtoValidationException(string message, Exception inner) : base(message, inner) { }
    }
}