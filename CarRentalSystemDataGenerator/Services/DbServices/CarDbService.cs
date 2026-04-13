using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class CarDbService : DbServiceBase<Car>
    {
        public CarDbService(AppDbContext db) : base(db)
        {
        }

        public override Car Get(int id)
        {
            var found = base._db.Cars.FirstOrDefault(c => c.CarID == id);
            if (found == null) throw new Exception($"Car with id {id} not found.");
            return found;
        }

        public override List<Car> GetAll()
        {
            var found = base._db.Cars.ToList();
            return found ?? new List<Car>();
        }

        public override Car Get(Car item)
        {
            var found = base._db.Cars.FirstOrDefault(c => c.LicensePlate == item.LicensePlate);
            if (found == null) throw new Exception($"Car with license plate {item.LicensePlate} not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Cars.Any(c => c.CarID == id);
        }

        public override Car Add(Car item)
        {
            if (item == null) throw new Exception("Car cannot be null.");
            BeginTransaction();
            try
            {
                base._db.Cars.Add(item);
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error adding car: {ex.Message}");
            }
            CommitTransaction();
            return item;
        }

        public override int AddMany(List<Car> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Car cannot be null.");
                    base._db.Cars.Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error adding car: {ex.Message}");
                    }
                }
            }
            CommitTransaction();
            return successCount;
        }

        public override Car Update(Car item)
        {
            if (item == null) throw new Exception("Car cannot be null.");
            var found = base._db.Cars.FirstOrDefault(c => c.CarID == item.CarID);
            if (found == null) throw new Exception($"Car with id {item.CarID} not found.");
            BeginTransaction();
            try
            {
                found.ModelID = item.ModelID;
                found.OfficeID = item.OfficeID;
                found.LicensePlate = item.LicensePlate;
                found.Year = item.Year;
                found.DailyRate = item.DailyRate;
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating car: {ex.Message}");
            }
            return found;
        }

        public override Car Update(int id, Car item)
        {
            if (item == null) throw new Exception("Car cannot be null.");
            var found = base._db.Cars.FirstOrDefault(c => c.CarID == id);
            if (found == null) throw new Exception($"Car with id {id} not found.");
            BeginTransaction();
            try
            {
                found.ModelID = item.ModelID;
                found.OfficeID = item.OfficeID;
                found.LicensePlate = item.LicensePlate;
                found.Year = item.Year;
                found.DailyRate = item.DailyRate;
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating car: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Car> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Car cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error updating car: {ex.Message}");
                    }
                }
            }
            CommitTransaction();
            return successCount;
        }

        public override bool Delete(int id)
        {
            BeginTransaction();
            try
            {
                var entity = base._db.Cars.FirstOrDefault(c => c.CarID == id);
                if (entity != null) base._db.Cars.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting car: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Car item)
        {
            BeginTransaction();
            try
            {
                var entity = base._db.Cars.FirstOrDefault(c => c.CarID == item.CarID);
                if (entity != null) base._db.Cars.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting car: {ex.Message}");
            }
            return true;
        }
    }
}
