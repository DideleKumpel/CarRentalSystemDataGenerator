using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class CustomerDbService : DbServiceBase<Customer>
    {
        protected CustomerDbService(AppDbContext db) : base(db)
        {
        }

        public override Customer Get(int id)
        {
            var found = base._db.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (found == null) throw new Exception($"Customer with id {id} not found.");
            return found;
        }

        public override List<Customer> GetAll()
        {
            var found = base._db.Customers.ToList();
            return found ?? new List<Customer>();
        }

        public override Customer Get(Customer item)
        {
            var found = base._db.Customers.FirstOrDefault(c => c.Email == item.Email);
            if (found == null) throw new Exception($"Customer with email {item.Email} not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Customers.Any(c => c.CustomerID == id);
        }

        public override Customer Add(Customer item)
        {
            if (item == null) throw new Exception("Customer cannot be null.");
            base.BeginTransaction();
            try
            {
                base._db.Customers.Add(item);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error adding customer: {ex.Message}");
            }
            return item;
        }

        public override int AddMany(List<Customer> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Customer cannot be null.");
                    Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error adding customer: {ex.Message}");
                    }
                }
            }
            base._db.SaveChanges();
            return successCount;
        }

        public override Customer Update(Customer item)
        {
            if (item == null) throw new Exception("Customer cannot be null.");
            var found = base._db.Customers.FirstOrDefault(c => c.CustomerID == item.CustomerID);
            if (found == null) throw new Exception($"Customer with id {item.CustomerID} not found.");
            base.BeginTransaction();
            try
            {
                found.AddressID = item.AddressID;
                found.FirstName = item.FirstName;
                found.LastName = item.LastName;
                found.Email = item.Email;
                found.DriverLicenseNum = item.DriverLicenseNum;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating customer: {ex.Message}");
            }
            return found;
        }

        public override Customer Update(int id, Customer item)
        {
            if (item == null) throw new Exception("Customer cannot be null.");
            var found = base._db.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (found == null) throw new Exception($"Customer with id {id} not found.");
            base.BeginTransaction();
            try
            {
                found.AddressID = item.AddressID;
                found.FirstName = item.FirstName;
                found.LastName = item.LastName;
                found.Email = item.Email;
                found.DriverLicenseNum = item.DriverLicenseNum;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating customer: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Customer> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Customer cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error updating customer: {ex.Message}");
                    }
                }
            }
            base._db.SaveChanges();
            return successCount;
        }

        public override bool Delete(int id)
        {
            base.BeginTransaction();
            try
            {
                var entity = base._db.Customers.FirstOrDefault(c => c.CustomerID == id);
                if (entity != null) base._db.Customers.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting customer: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Customer item)
        {
            base.BeginTransaction();
            try
            {
                var entity = base._db.Customers.FirstOrDefault(c => c.CustomerID == item.CustomerID);
                if (entity != null) base._db.Customers.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting customer: {ex.Message}");
            }
            return true;
        }
    }
}
