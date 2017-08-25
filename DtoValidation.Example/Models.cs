using System;

namespace DtoValidation.Example
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }

    public class Address
    {
        public Guid PersonId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
    }

    public class Contact
    {
        public Guid CustomerId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
    }

    public class DtoContact
    {
        public string number { get; set; }
        public string type { get; set; }
    }

    public class DtoCustomer
    {
        public string name { get; set; }
        public DateTime birthday { get; set; }

        public string address_street { get; set; }
        public string address_city { get; set; }
        public int address_numberm { get; set; }
        public string address_complement { get; set; }

        public DtoContact[] contacts { get; set; }

        public bool is_famous { get; set; }
    }
}
