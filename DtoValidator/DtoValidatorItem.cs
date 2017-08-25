using System;

namespace DtoValidation
{
    internal class DtoValidatorItem<TProperty>
    {
        public object Expected { get; private set; }
        public object Value { get; private set; }
        public string Name { get; private set; }

        readonly Func<object, object, bool> _equalityValidator;

        public DtoValidatorItem(string name, object value, Func<TProperty, bool> equalityValidator)
        {
            Name = name;
            Value = value;

            _equalityValidator = (e,v) => equalityValidator.Invoke((TProperty)v);
        }

        public DtoValidatorItem(string name, object value, object expected, Func<object, object, bool> equalityValidator)
        {
            Expected = expected;
            Name = name;
            Value = value;

            _equalityValidator = equalityValidator;
        }

        public void Validate()
        {
            if (!_equalityValidator.Invoke(Expected, Value))
                throw new UnequalPropertyException(Expected, Value, Name);
        }
    }
}