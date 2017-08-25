using System;

namespace DtoValidation
{
    [Serializable]
    public class UnequalPropertyException: GenericDtoValidationException
    {
        public UnequalPropertyException(object expected, object value, string name)
            : base(FormatMessage(expected, value, name))
        {

        }

        static string FormatMessage(object expected, object value, string name)
        {
            return $"Property '{name}' Expected: '{expected}' Received: '{value}'";
        }
    }
}