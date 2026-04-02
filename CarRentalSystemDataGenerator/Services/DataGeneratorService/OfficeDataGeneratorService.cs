using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class OfficeDataGeneratorService : DataGeneratorServiceBase<Office>
    {
        public OfficeDataGeneratorService() : base() { }
        public OfficeDataGeneratorService(int seed) : base(seed) { }

        public List<Office> GenerateData(int Amount, List<Address> addresses)
        {
            if (addresses == null || addresses.Count == 0) throw new ArgumentException("addresses must contain at least one Address");
            var list = new List<Office>();
            for (int i = 0; i < Amount; i++)
            {
                var addr = addresses[Random.Next(addresses.Count)];
                list.Add(new Office { AddressID = addr.AddressID, OfficeName = "Office " + (i + 1) });
            }
            return list;
        }
    }
}
