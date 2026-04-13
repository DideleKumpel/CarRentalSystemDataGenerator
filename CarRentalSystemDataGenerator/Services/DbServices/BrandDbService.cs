using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class BrandDbService : DbServiceBase<Brand>
    {
        public BrandDbService(AppDbContext db) : base(db)
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
        override public List<Brand> GetAll()
        {
            List<Brand> foundBrands = base._db.Brands.ToList();
            if(foundBrands == null)
            {
                foundBrands = new List<Brand>();
            }
            return foundBrands;
        }
        override public Brand Get(Brand item)
        {
            Brand found = base._db.Brands.FirstOrDefault(b => b.Name == item.Name);
            if (found == null)             {
                throw new Exception($"Brand with name {item.Name} not found.");
            }
            return found;
        }
        override public bool Exists(int id)
        {
            Brand found = base._db.Brands.FirstOrDefault(b => b.BrandID == id);
            if (found == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Create
        override public Brand Add(Brand item)
        {
            
            if (item == null)
            {
                throw new Exception("Brand cannot be null.");
            }
            BeginTransaction();;
            try
            {
                base._db.Brands.Add(item);
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error adding brand: {ex.Message}");
            }
            CommitTransaction();
            return item;
        }
        override public int AddMany(List<Brand> items, bool CancelOnError) {
            BeginTransaction();;
            int successCount = 0;
            foreach (Brand item in items)
            {
                try
                {
                    if (item == null)
                    {
                        throw new Exception("Brand cannot be null.");
                    }
                    base._db.Brands.Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error adding brand: {ex.Message}");
                    }
                }
            }
            CommitTransaction();
            return successCount;
        }

        //Update
        override public Brand Update(Brand item)
        {
            if (item == null)
            {
                throw new Exception("Brand cannot be null.");
            }
            Brand found = base._db.Brands.FirstOrDefault(b => b.BrandID == item.BrandID);
            if (found == null)
            {
                throw new Exception($"Brand with id {item.BrandID} not found.");
            }
            BeginTransaction();;
            try
            {
                found.Name = item.Name;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating brand: {ex.Message}");
            }
            return found;
        }
        override public Brand Update(int id, Brand item)
        {
            if (item == null)
            {
                throw new Exception("Brand cannot be null.");
            }
            Brand found = base._db.Brands.FirstOrDefault(b => b.BrandID == id);
            if (found == null)
            {
                throw new Exception($"Brand with id {id} not found.");
            }
            BeginTransaction();;
            try
            {
                found.Name = item.Name;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating brand: {ex.Message}");
            }
            return found;
        }
        override public int UpdateMany(List<Brand> items, bool CancelOnError)
        {
            BeginTransaction();;
            int successCount = 0;
            foreach (Brand item in items)
            {
                try
                {
                    if (item == null)
                    {
                        throw new Exception("Brand cannot be null.");
                    }
                    base._db.Brands.Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error updating brand: {ex.Message}");
                    }
                }
            }
            base._db.SaveChanges();
            return successCount;
        }

        //Delete
        override public bool Delete(int id)
        {
            BeginTransaction();;
            try
            {
                base._db.Brands.Remove(base._db.Brands.FirstOrDefault(b => b.BrandID == id));
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting brand: {ex.Message}");
            }
             return true;
        }
        override public bool Delete(Brand item)
        {
            BeginTransaction();;
            try
            {
                base._db.Brands.Remove(base._db.Brands.FirstOrDefault(b => b.BrandID == item.BrandID));
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting brand: {ex.Message}");
            }
            return true;
        }
    }
}
