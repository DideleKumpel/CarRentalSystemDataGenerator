using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class MaintenanceDataGeneratorService : DataGeneratorServiceBase<Maintenance>
    {
        public MaintenanceDataGeneratorService() : base() { }
        public MaintenanceDataGeneratorService(int seed) : base(seed) { }

        public List<Maintenance> GenerateData(int Amount, List<Car> cars)
        {
            if (cars == null || cars.Count == 0) throw new ArgumentException("cars must contain at least one Car");
            var list = new List<Maintenance>();
            var descriptions = new[] { "Oil change", "Brake replacement", "Tire rotation", "Engine check", "Battery replacement" };
            for (int i = 0; i < Amount; i++)
            {
                var car = cars[Random.Next(cars.Count)];
                list.Add(new Maintenance { CarID = car.CarID, Description = descriptions[Random.Next(descriptions.Length)], MaintenanceDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-Random.Next(0, 365))), Cost = (decimal)(Random.Next(50, 2000) + Random.NextDouble()) });
            }
            return list;
        }
    }
}
