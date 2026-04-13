using CarRentalSystemDataGenerator.DB.Entities;
using CarRentalSystemDataGenerator.DB.Enums;
using System;
using System.Collections.Generic;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class PaymentDataGeneratorService : DataGeneratorServiceBase<Payment>
    {
        public PaymentDataGeneratorService() : base() { }
        public PaymentDataGeneratorService(int seed) : base(seed) { }

        public List<Payment> GenerateData(int Amount, List<Rental> rentals)
        {
            if (rentals == null || rentals.Count == 0) throw new ArgumentException("rentals must contain at least one Rental");
            var list = new List<Payment>();
            var methods = Enum.GetValues(typeof(PaymentMethod));
            for (int i = 0; i < Amount; i++)
            {
                var rental = rentals[Random.Next(rentals.Count)];
                list.Add(new Payment { RentalID = rental.RentalID, Amount = (decimal)(Random.Next(10, 2000) + Random.NextDouble()), PaymentDate = DateTime.UtcNow.AddDays(-Random.Next(0, 365)), Method = (PaymentMethod)methods.GetValue(Random.Next(methods.Length))! });
            }
            return list;
        }
    }
}
