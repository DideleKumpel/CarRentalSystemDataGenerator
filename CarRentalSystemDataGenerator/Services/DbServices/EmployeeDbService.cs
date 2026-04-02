using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class EmployeeDbService : DbServiceBase<Employee>
    {
        protected EmployeeDbService(AppDbContext db) : base(db)
        {
        }

        public override Employee Get(int id)
        {
            var found = base._db.Employees.FirstOrDefault(e => e.EmployeeID == id);
            if (found == null) throw new Exception($"Employee with id {id} not found.");
            return found;
        }

        public override List<Employee> GetAll()
        {
            var found = base._db.Employees.ToList();
            return found ?? new List<Employee>();
        }

        public override Employee Get(Employee item)
        {
            var found = base._db.Employees.FirstOrDefault(e => e.FirstName == item.FirstName && e.LastName == item.LastName);
            if (found == null) throw new Exception($"Employee {item.FirstName} {item.LastName} not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Employees.Any(e => e.EmployeeID == id);
        }

        public override Employee Add(Employee item)
        {
            if (item == null) throw new Exception("Employee cannot be null.");
            base.BeginTransaction();
            try
            {
                base._db.Employees.Add(item);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error adding employee: {ex.Message}");
            }
            return item;
        }

        public override int AddMany(List<Employee> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Employee cannot be null.");
                    Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error adding employee: {ex.Message}");
                    }
                }
            }
            base._db.SaveChanges();
            return successCount;
        }

        public override Employee Update(Employee item)
        {
            if (item == null) throw new Exception("Employee cannot be null.");
            var found = base._db.Employees.FirstOrDefault(e => e.EmployeeID == item.EmployeeID);
            if (found == null) throw new Exception($"Employee with id {item.EmployeeID} not found.");
            base.BeginTransaction();
            try
            {
                found.OfficeID = item.OfficeID;
                found.FirstName = item.FirstName;
                found.LastName = item.LastName;
                found.Position = item.Position;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating employee: {ex.Message}");
            }
            return found;
        }

        public override Employee Update(int id, Employee item)
        {
            if (item == null) throw new Exception("Employee cannot be null.");
            var found = base._db.Employees.FirstOrDefault(e => e.EmployeeID == id);
            if (found == null) throw new Exception($"Employee with id {id} not found.");
            base.BeginTransaction();
            try
            {
                found.OfficeID = item.OfficeID;
                found.FirstName = item.FirstName;
                found.LastName = item.LastName;
                found.Position = item.Position;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating employee: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Employee> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Employee cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error updating employee: {ex.Message}");
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
                var entity = base._db.Employees.FirstOrDefault(e => e.EmployeeID == id);
                if (entity != null) base._db.Employees.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting employee: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Employee item)
        {
            base.BeginTransaction();
            try
            {
                var entity = base._db.Employees.FirstOrDefault(e => e.EmployeeID == item.EmployeeID);
                if (entity != null) base._db.Employees.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting employee: {ex.Message}");
            }
            return true;
        }
    }
}
