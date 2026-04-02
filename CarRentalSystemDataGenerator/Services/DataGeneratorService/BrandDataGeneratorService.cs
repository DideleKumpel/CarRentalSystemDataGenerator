using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class BrandDataGeneratorService : DataGeneratorServiceBase<Brand>
    {
        public BrandDataGeneratorService() : base() { }
        public BrandDataGeneratorService(int seed) : base(seed) { }

        public new List<Brand> GenerateData(int Amount)
        {
            var list = new List<Brand>();
            var names = new[] { "Toyota", "Ford", "Honda", "Chevrolet", "BMW", "Audi", "Mercedes", "Kia", "Hyundai", "Nissan" };
            for (int i = 0; i < Amount; i++)
            {
                list.Add(new Brand { Name = names[Random.Next(names.Length)] });
            }
            return list;
        }
    }
}
