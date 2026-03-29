using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services
{
    internal class BrandDbService : DbServiceBase<Brand>
    {
        protected BrandDbService(AppDbContext db) : base(db)
        {
        }
        //Get
        override public Brand Get(int id)
        {
            Brand found = base._db.Brands.FirstOrDefault(b => b.BrandID == id);
            if(found == null)
            {
                throw new Exception($"Brand with id {id} not found.");
            }
            return found;
        }
        List<Brand> GetAll()
        {
            List<Brand> foundBrands = base._db.Brands.ToList();
            if(foundBrands == null)
            {
                foundBrands = new List<Brand>();
            }
            return foundBrands;
        }
        Brand Get(Brand item)
        {
            Brand found = base._db.Brands.FirstOrDefault(b => b.Name == item.Name);
            if (found == null)             {
                throw new Exception($"Brand with name {item.Name} not found.");
            }
            return found;
        }
        bool Exists(int id);

        //Create
        T Add(T item);
        int AddMany(List<T> items, bool CancelOnError);

        //Update
        T Update(T item);
        T Update(int id, T item);
        int UpdateMany(List<T> items, bool CancelOnError);

        //Delete
        bool Delete(int id);
        bool Delete(T item);

        //Transations
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
