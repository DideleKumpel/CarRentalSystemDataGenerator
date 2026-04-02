using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class ModelDataGeneratorService : DataGeneratorServiceBase<Model>
    {
        public ModelDataGeneratorService() : base() { }
        public ModelDataGeneratorService(int seed) : base(seed) { }

        // requires list of brands to link to
        public List<Model> GenerateData(int Amount, List<Brand> brands)
        {
            if (brands == null || brands.Count == 0) throw new ArgumentException("brands must contain at least one Brand");
            var list = new List<Model>();
            var sampleNames = new[] { "S", "X", "A", "GT", "Sport", "Sedan", "Coupe", "Hatch" };
            for (int i = 0; i < Amount; i++)
            {
                var brand = brands[Random.Next(brands.Count)];
                list.Add(new Model { BrandID = brand.BrandID, Name = brand.Name + " " + sampleNames[Random.Next(sampleNames.Length)] + " " + Random.Next(1, 999) });
            }
            return list;
        }
    }
}
