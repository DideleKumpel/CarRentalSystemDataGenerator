using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class AddressDbService : DbServiceBase<Address>
    {

        public AddressDbService(AppDbContext db) : base(db)
        {
        }

        //Get
        override public Address Get(int id)
        {
            Address address = base._db.Addresses.Where(a => a.AddressID == id).FirstOrDefault();
            if (address == null)
            {
                throw new Exception($"Address with id {id} not found.");
            }
            return address;
        }
        override public List<Address> GetAll()
        {
            List<Address> AdressList = base._db.Addresses.ToList();
            if (AdressList == null)
            {
                AdressList = new List<Address>();
            }
            return AdressList;
        }

        override public Address Get(Address item)
        {
            Address address = base._db.Addresses.Where(a => a.City == item.City && a.Street == item.Street && a.HouseNumber == item.HouseNumber && a.PostalCode == item.PostalCode).FirstOrDefault();
            if (address == null)
            {
                throw new Exception($"Address with city {item.City}, street {item.Street}, house number {item.HouseNumber} and postal code {item.PostalCode} not found.");
            }
            return address;
        }
        override public bool Exists(int id)
        {
            
            if (base._db.Addresses.Any(a => a.AddressID == id))
            {
                return false;
            }else { 
                return true;
            }
        }

        //Create
        override public Address Add(Address item)
        {
            BeginTransaction();
            try
            {
                base._db.Addresses.Add(item);
                CommitTransaction();
                return item;
            }
            catch (Exception ex) {
                RollbackTransaction();
                throw ex;
            }
        }
        override public int AddMany(List<Address> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (Address item in items)
            {
                try
                {
                    base._db.Addresses.Add(item);
                }
                catch (Exception ex) {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        return 0;
                    }
                }
                successCount++;
            }
            CommitTransaction();
            return successCount;
        }

        //Update
        override public Address Update(Address item) { 
            BeginTransaction();
            try
            {
                base._db.Addresses.Update(item);
                CommitTransaction();
                return item;

            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }
        override public Address Update(int id, Address item)
        {
            BeginTransaction();
            try
            {
                Address address = base._db.Addresses.Where(a => a.AddressID == id).FirstOrDefault();
                if (address == null)
                {
                    throw new Exception($"Address with id {id} not found.");
                }
                address.City = item.City;
                address.Street = item.Street;
                address.HouseNumber = item.HouseNumber;
                address.PostalCode = item.PostalCode;
                base._db.Addresses.Update(address);
                CommitTransaction();
                return address;

            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }

        override public int UpdateMany(List<Address> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (Address item in items) {
                try
                {
                    Update(item);
                    successCount++;
                }
                catch (Exception ex) {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw ex;
                    }
                }
            }
            return successCount;
        }

        //Delete
        override public bool Delete(int id) { 
            BeginTransaction();
            try
            {
                Address address = base._db.Addresses.Where(a => a.AddressID == id).FirstOrDefault();
                if (address == null)
                {
                    throw new Exception($"Address with id {id} not found.");
                }
                base._db.Addresses.Remove(address);
                CommitTransaction();
                return true;

            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
        }
        override public bool Delete(Address item) {
            BeginTransaction();
            try
            {
                base._db.Addresses.Remove(item);
                CommitTransaction();
                return true;
            }catch (Exception ex){  
                throw ex; 
            }
        }
    }
}
