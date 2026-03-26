using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services
{
    internal class AddressDbService: DbServiceInterface<Address>
    {
        private AppDbContext DbContext;

        //Get
        Address Get(int id)
        {
            Address address = DbContext.Addresses.Where(a => a.AddressID == id).FirstOrDefault();
            if (address == null)
            {
                throw new Exception($"Address with id {id} not found.");
            }
            return address;
        }
        List<Address> GetAll()
        {
            List<Address> AdressList = DbContext.Addresses.ToList();
            if (AdressList == null)
            {
                AdressList = new List<Address>();
            }
            return AdressList;
        }

        Address Get(Address item)
        {
            Address address = DbContext.Addresses.Where(a => a.City == item.City && a.Street == item.Street && a.HouseNumber == item.HouseNumber && a.PostalCode == item.PostalCode).FirstOrDefault();
            if (address == null)
            {
                throw new Exception($"Address with city {item.City}, street {item.Street}, house number {item.HouseNumber} and postal code {item.PostalCode} not found.");
            }
            return address;
        }
        bool Exists(int id)
        {
            
            if (DbContext.Addresses.Any(a => a.AddressID == id) == null)
            {
                return false;
            }else { 
                return true;
            }
        }

    }
}
