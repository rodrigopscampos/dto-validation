using System;

namespace DtoValidation
{

    [Serializable]
    public class PropertiesNotValidatedException : GenericDtoValidationException
    {
        public PropertiesNotValidatedException(params string[] propertyName)
            : base(FormatMessage(propertyName)) { }

        static string FormatMessage(params string[] propertyName)
        {
            return $"Properties '{string.Join(", ", propertyName)}' was not validated or ignored";
        }
    }
}