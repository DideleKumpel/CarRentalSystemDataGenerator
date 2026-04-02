using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services
{
    internal class MaintenanceDbService : DbServiceBase<Maintenance>
    {
        protected MaintenanceDbService(AppDbContext db) : base(db)
        {
        }

        public override Maintenance Get(int id)
        {
            var found = base._db.Maintenances.FirstOrDefault(m => m.MaintenanceID == id);
            if (found == null) throw new Exception($"Maintenance with id {id} not found.");
            return found;
        }

        public override List<Maintenance> GetAll()
        {
            var found = base._db.Maintenances.ToList();
            return found ?? new List<Maintenance>();
        }

        public override Maintenance Get(Maintenance item)
        {
            var found = base._db.Maintenances.FirstOrDefault(m => m.MaintenanceID == item.MaintenanceID);
            if (found == null) throw new Exception($"Maintenance not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Maintenances.Any(m => m.MaintenanceID == id);
        }

        public override Maintenance Add(Maintenance item)
        {
            if (item == null) throw new Exception("Maintenance cannot be null.");
            base.BeginTransaction();
            try
            {
                base._db.Maintenances.Add(item);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error adding maintenance: {ex.Message}");
            }
            return item;
        }

        public override int AddMany(List<Maintenance> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Maintenance cannot be null.");
                    Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error adding maintenance: {ex.Message}");
                    }
                }
            }
            base._db.SaveChanges();
            return successCount;
        }

        public override Maintenance Update(Maintenance item)
        {
            if (item == null) throw new Exception("Maintenance cannot be null.");
            var found = base._db.Maintenances.FirstOrDefault(m => m.MaintenanceID == item.MaintenanceID);
            if (found == null) throw new Exception($"Maintenance with id {item.MaintenanceID} not found.");
            base.BeginTransaction();
            try
            {
                found.CarID = item.CarID;
                found.Description = item.Description;
                found.MaintenanceDate = item.MaintenanceDate;
                found.Cost = item.Cost;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating maintenance: {ex.Message}");
            }
            return found;
        }

        public override Maintenance Update(int id, Maintenance item)
        {
            if (item == null) throw new Exception("Maintenance cannot be null.");
            var found = base._db.Maintenances.FirstOrDefault(m => m.MaintenanceID == id);
            if (found == null) throw new Exception($"Maintenance with id {id} not found.");
            base.BeginTransaction();
            try
            {
                found.CarID = item.CarID;
                found.Description = item.Description;
                found.MaintenanceDate = item.MaintenanceDate;
                found.Cost = item.Cost;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating maintenance: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Maintenance> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Maintenance cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error updating maintenance: {ex.Message}");
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
                var entity = base._db.Maintenances.FirstOrDefault(m => m.MaintenanceID == id);
                if (entity != null) base._db.Maintenances.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting maintenance: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Maintenance item)
        {
            base.BeginTransaction();
            try
            {
                var entity = base._db.Maintenances.FirstOrDefault(m => m.MaintenanceID == item.MaintenanceID);
                if (entity != null) base._db.Maintenances.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting maintenance: {ex.Message}");
            }
            return true;
        }
    }
}
