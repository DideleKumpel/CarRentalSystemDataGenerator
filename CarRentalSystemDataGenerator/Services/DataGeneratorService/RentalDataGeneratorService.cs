using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class RentalDataGeneratorService : DataGeneratorServiceBase<Rental>
    {
        public RentalDataGeneratorService() : base() { }
        public RentalDataGeneratorService(int seed) : base(seed) { }

        public List<Rental> GenerateData(int Amount, List<Car> cars, List<Customer> customers, List<Employee>? employees = null)
        {
            if (cars == null || cars.Count == 0) throw new ArgumentException("cars must contain at least one Car");
            if (customers == null || customers.Count == 0) throw new ArgumentException("customers must contain at least one Customer");
            var list = new List<Rental>();
            for (int i = 0; i < Amount; i++)
            {
                var car = cars[Random.Next(cars.Count)];
                var customer = customers[Random.Next(customers.Count)];
                Employee? emp = null;
                if (employees != null && employees.Count > 0)
                {
                    emp = employees[Random.Next(employees.Count)];
                }
                var rentalDate = DateTime.Now.AddDays(-Random.Next(0, 365));
                DateTime? returnDate = rentalDate.AddDays(Random.Next(1, 30));
                list.Add(new Rental { CarID = car.CarID, CustomerID = customer.CustomerID, EmployeeID = emp?.EmployeeID, RentalDate = rentalDate, ReturnDate = returnDate, TotalCost = (decimal)(Random.Next(100, 5000) + Random.NextDouble()) });
            }
            return list;
        }
    }
}
