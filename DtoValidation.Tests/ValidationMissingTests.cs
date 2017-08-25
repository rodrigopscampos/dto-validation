using DtoValidation.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DtoValidation.Tests
{
    [TestClass]
    public class ValidationMissingTests
    {
        static SomeDto _someDto = new SomeDto
        {
            I = 0,
            S = "",
            Dt = DateTime.Now
        };

        DtoValidator<SomeDto> _testingObject = new DtoValidator<SomeDto>(_someDto);

        [TestMethod]
        public void OnMissingPropertyValidation_ShouldThrowPropertiesNotValidatedException()
        {
            _testingObject.AddValidation(dto => dto.I, 0);

            var success = false;

            try
            {
                _testingObject.RunValidation();
            }
            catch (PropertiesNotValidatedException)
            {
                success = true;
            }

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void OnPropertyValidationIgnored_ShouldNotThrowPropertiesNotValidatedException()
        {
            _testingObject.IgnoreValidation(dto => dto.I);
            _testingObject.IgnoreValidation(dto => dto.S);
            _testingObject.IgnoreValidation(dto => dto.Dt);

            _testingObject.RunValidation();
        }
    }
}