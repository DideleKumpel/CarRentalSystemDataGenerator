using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.DB.Enums;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class EmployeeDataGeneratorService : DataGeneratorServiceBase<Employee>
    {
        public EmployeeDataGeneratorService() : base() { }
        public EmployeeDataGeneratorService(int seed) : base(seed) { }

        public List<Employee> GenerateData(int Amount, List<Office> offices)
        {
            if (offices == null || offices.Count == 0) throw new ArgumentException("offices must contain at least one Office");
            var list = new List<Employee>();
            var firstNames = new[] { "Anna", "Piotr", "Kasia", "Marek", "Ewa", "Tomasz" };
            var lastNames = new[] { "Kowalski", "Nowak", "Lewandowski", "Wójcik", "Kamiński" };
            var positions = Enum.GetValues(typeof(EmployeePosition));
            for (int i = 0; i < Amount; i++)
            {
                var office = offices[Random.Next(offices.Count)];
                var fn = firstNames[Random.Next(firstNames.Length)];
                var ln = lastNames[Random.Next(lastNames.Length)];
                list.Add(new Employee { OfficeID = office.OfficeID, FirstName = fn, LastName = ln, Position = (EmployeePosition)positions.GetValue(Random.Next(positions.Length))! });
            }
            return list;
        }
    }
}
