using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class CustomerDataGeneratorService : DataGeneratorServiceBase<Customer>
    {
        public CustomerDataGeneratorService() : base() { }
        public CustomerDataGeneratorService(int seed) : base(seed) { }

        public List<Customer> GenerateData(int Amount, List<Address> addresses)
        {
            if (addresses == null || addresses.Count == 0) throw new ArgumentException("addresses must contain at least one Address");
            var list = new List<Customer>();
            for (int i = 0; i < Amount; i++)
            {
                var addr = addresses[Random.Next(addresses.Count)];
                var fn = GenerateFirstName();
                var ln = GenerateLastName();
                list.Add(new Customer
                {
                    AddressID = addr.AddressID,
                    FirstName = fn,
                    LastName = ln,
                    Email = GenerateEmail(fn, ln),
                    DriverLicenseNum = GenerateDriverLicense()
                });
            }
            return list;
        }

        private string GenerateFirstName()
        {
            string[] firstNames = { "John", "Jane", "Alex", "Chris", "Pat", "Sam", "Taylor", "Jordan" };
            return firstNames[Random.Next(firstNames.Length)];
        }

        private string GenerateLastName()
        {
            string[] lastNames = { "Smith", "Johnson", "Brown", "Williams", "Jones", "Miller", "Davis" };
            return lastNames[Random.Next(lastNames.Length)];
        }

        private string GenerateEmail(string firstName, string lastName)
        {
            return firstName.ToLower() + "." + lastName.ToLower() + "@example.com";
        }

        private string GenerateDriverLicense()
        {
            return "DL" + Random.Next(100000, 999999).ToString();
        }
    }
}

