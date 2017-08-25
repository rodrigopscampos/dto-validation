using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DtoValidation
{
    public class DtoValidator<TDto>
    {
        readonly TDto _entity;
        readonly Dictionary<string, Action> _validations = new Dictionary<string, Action>();

        public static readonly Func<object, object, bool> DefaultEqualityValidator = (expected, value) => object.Equals(expected, value);

        public Func<object, object, bool> EqualityValidator { get; set; } = DefaultEqualityValidator;

        public void AddValidation<TProperty>(Expression<Func<TDto, TProperty>> propertyValue, Func<TProperty, bool> equalityValidator)
        {
            var memberName = ((MemberExpression)propertyValue.Body).Member.Name;

            var validator = new DtoValidatorItem<TProperty>(memberName, propertyValue.Compile().Invoke(_entity), equalityValidator);
            _validations.Add(validator.Name, validator.Validate);
        }

        public void AddValidation<TProperty>(Expression<Func<TDto, TProperty>> propertyValue, TProperty value)
        {
            var memberName = ((MemberExpression)propertyValue.Body).Member.Name;

            var validator = new DtoValidatorItem<TProperty>(memberName, propertyValue.Compile().Invoke(_entity), value, EqualityValidator);
            _validations.Add(validator.Name, validator.Validate);
        }

        public void IgnoreValidation<TProperty>(Expression<Func<TDto, TProperty>> propertyValue)
        {
            var memberName = ((MemberExpression)propertyValue.Body).Member.Name;

            var validator = new DtoValidatorItem<TProperty>(memberName, null, null, EqualityValidator);
            _validations.Add(validator.Name, validator.Validate);
        }

        public DtoValidator(TDto entity)
        {
            _entity = entity;
        }

        public void RunValidation()
        {
            var properties = typeof(TDto).GetProperties().Select(p => p.Name).ToArray();

            var notValidatedProperties = properties.Where(p => !_validations.Keys.Contains(p)).ToArray();

            if (notValidatedProperties.Any())
                throw new PropertiesNotValidatedException(notValidatedProperties);

            foreach (var item in _validations)
                item.Value.Invoke();
        }
    }
}