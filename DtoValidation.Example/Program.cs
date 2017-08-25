using System;
using System.Linq;

namespace DtoValidation.Example
{
    class Program
    {
        static DtoCustomer _dtoPerson;
        static Contact[] _contacts;
        static Address _address;
        static Customer _person;

        static void Main(string[] args)
        {
            FillDomain();

            FillDto();

            PerformValidation();
        }

        static void FillDomain()
        {
            _person = new Customer
            {
                Id = Guid.NewGuid(),
                Birthday = new DateTime(2000, 1, 1),
                Name = "Nome da pessoa"
            };

            _address = new Address
            {
                PersonId = _person.Id,
                City = "Cidade",
                Complement = "Complemento",
                Number = 0,
                Street = "Rua"
            };

            _contacts = new[]
            {
                new Contact { CustomerId = _person.Id, Number = "00000000", Type = "Residencial" },
                new Contact { CustomerId = _person.Id, Number = "11111111", Type = "Celular" },
            };
        }

        static void FillDto()
        {
            _dtoPerson = new DtoCustomer
            {
                birthday = _person.Birthday,
                name = _person.Name,

                address_city = _address.City,
                address_complement = _address.Complement,
                address_numberm = _address.Number,
                address_street = _address.Street,

                contacts = _contacts.Select(c => new DtoContact { number = c.Number, type = c.Type }).ToArray()
            };
        }

        static void PerformValidation()
        {
            var dtoValidator = new DtoValidator<DtoCustomer>(_dtoPerson);

            dtoValidator.AddValidation(dto => dto.address_city, _address.City);
            dtoValidator.AddValidation(dto => dto.address_complement, _address.Complement);
            dtoValidator.AddValidation(dto => dto.address_numberm, _address.Number);
            dtoValidator.AddValidation(dto => dto.address_street, _address.Street);
            dtoValidator.AddValidation(dto => dto.birthday, _person.Birthday);
            dtoValidator.AddValidation(dto => dto.name, _person.Name);

            dtoValidator.IgnoreValidation(dto => dto.is_famous);

            dtoValidator.AddValidation(dto => dto.contacts, ValidateContacts);

            dtoValidator.RunValidation();
        }

        static bool ValidateContacts(DtoContact[] contacts)
        {
            for (int i = 0; i < contacts.Length; i++)
            {
                try
                {
                    var dtoValidation = new DtoValidator<DtoContact>(contacts[i]);

                    dtoValidation.AddValidation(c => c.number, contacts[i].number);
                    dtoValidation.AddValidation(c => c.type, contacts[i].type);

                    dtoValidation.RunValidation();
                }
                catch (GenericDtoValidationException)
                {
                    return false;
                }
            }

            return true;
        }
    }
}