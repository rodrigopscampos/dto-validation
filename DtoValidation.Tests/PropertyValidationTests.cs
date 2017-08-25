using DtoValidation.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DtoValidation.Tests
{
    [TestClass]
    public class PropertyValidationTests
    {
        static Random _random = new Random();

        static SomeDto _someDto = new SomeDto
        {
            I = _random.Next(),
            S = _random.Next().ToString(),
            Dt = DateTime.Now
        };

        DtoValidator<SomeDto> _testingObject = new DtoValidator<SomeDto>(_someDto);

        [TestMethod]
        public void OnValidationEqualValues_ShouldNotThrowException()
        {
            _testingObject.AddValidation(dto => dto.I, _someDto.I);
            _testingObject.AddValidation(dto => dto.S, _someDto.S);
            _testingObject.AddValidation(dto => dto.Dt, _someDto.Dt);

            _testingObject.RunValidation();
        }
    }
}