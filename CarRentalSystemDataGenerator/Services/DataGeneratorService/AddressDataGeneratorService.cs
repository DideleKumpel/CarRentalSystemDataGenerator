using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class AddressDataGeneratorService : DataGeneratorServiceBase<Address>
    {
        AddressDataGeneratorService() : base() { }
        AddressDataGeneratorService(int seed) : base(seed) { }

        string GenerateCity()
        {
            List<string> cities = new List<string> {
                "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" ,
                "Austin", "Jacksonville", "Fort Worth", "Columbus", "San Francisco", "Charlotte", "Indianapolis", "Seattle", "Denver", "Washington D.C.", "Boston"
            };
            return cities[Random.Next(cities.Count)];
        }

        String GenerateStreet()
        {
            List<string> streets = new List<string> {
                "Main St", "High St", "Park Ave", "Oak St", "Pine St", "Maple St", "Cedar St", "Elm St", "Washington St", "Lake St",
                "Hill St", "River Rd", "Sunset Blvd", "Broadway", "5th Ave", "6th Ave", "7th Ave", "8th Ave", "9th Ave", "10th Ave"
            };
            return streets[Random.Next(streets.Count)];
        }

        String GenerateHouseNumber()
        {
            return Random.Next(1, 1000).ToString();
        }

        String GeneratePostalCode()
        {
            return $"{Random.Next(10, 99).ToString()}-{Random.Next(100, 999).ToString()}";
        }
    }
}
