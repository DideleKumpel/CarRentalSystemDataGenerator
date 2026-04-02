using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class CarDataGeneratorService : DataGeneratorServiceBase<Car>
    {
        public CarDataGeneratorService() : base() { }
        public CarDataGeneratorService(int seed) : base(seed) { }

        public List<Car> GenerateData(int Amount, List<Model> models, List<Office> offices)
        {
            if (models == null || models.Count == 0) throw new ArgumentException("models must contain at least one Model");
            if (offices == null || offices.Count == 0) throw new ArgumentException("offices must contain at least one Office");
            var list = new List<Car>();
            for (int i = 0; i < Amount; i++)
            {
                var model = models[Random.Next(models.Count)];
                var office = offices[Random.Next(offices.Count)];
                list.Add(new Car { ModelID = model.ModelID, OfficeID = office.AddressID, LicensePlate = "PL" + Random.Next(1000, 9999), Year = Random.Next(2000, 2024), DailyRate = (decimal)(Random.Next(30, 200) + Random.NextDouble()) });
            }
            return list;
        }
    }
}
