using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class RentalDbService : DbServiceBase<Rental>
    {
        public RentalDbService(AppDbContext db) : base(db)
        {
        }

        public override Rental Get(int id)
        {
            var found = base._db.Rentals.FirstOrDefault(r => r.RentalID == id);
            if (found == null) throw new Exception($"Rental with id {id} not found.");
            return found;
        }

        public override List<Rental> GetAll()
        {
            var found = base._db.Rentals.ToList();
            return found ?? new List<Rental>();
        }

        public override Rental Get(Rental item)
        {
            var found = base._db.Rentals.FirstOrDefault(r => r.RentalID == item.RentalID);
            if (found == null) throw new Exception($"Rental not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Rentals.Any(r => r.RentalID == id);
        }

        public override Rental Add(Rental item)
        {
            if (item == null) throw new Exception("Rental cannot be null.");
            BeginTransaction();
            try
            {
                base._db.Rentals.Add(item);
				CommitTransaction();
			}
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error adding rental: {ex.Message}");
            }
            return item;
        }

        public override int AddMany(List<Rental> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Rental cannot be null.");
                    base._db.Rentals.Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error adding rental: {ex.Message}");
                    }
                }
            }
			CommitTransaction();
            return successCount;
        }

        public override Rental Update(Rental item)
        {
            if (item == null) throw new Exception("Rental cannot be null.");
            var found = base._db.Rentals.FirstOrDefault(r => r.RentalID == item.RentalID);
            if (found == null) throw new Exception($"Rental with id {item.RentalID} not found.");
            BeginTransaction();
            try
            {
                found.CarID = item.CarID;
                found.CustomerID = item.CustomerID;
                found.EmployeeID = item.EmployeeID;
                found.RentalDate = item.RentalDate;
                found.ReturnDate = item.ReturnDate;
                found.TotalCost = item.TotalCost;
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating rental: {ex.Message}");
            }
            return found;
        }

        public override Rental Update(int id, Rental item)
        {
            if (item == null) throw new Exception("Rental cannot be null.");
            var found = base._db.Rentals.FirstOrDefault(r => r.RentalID == id);
            if (found == null) throw new Exception($"Rental with id {id} not found.");
            BeginTransaction();
            try
            {
                found.CarID = item.CarID;
                found.CustomerID = item.CustomerID;
                found.EmployeeID = item.EmployeeID;
                found.RentalDate = item.RentalDate;
                found.ReturnDate = item.ReturnDate;
                found.TotalCost = item.TotalCost;
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating rental: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Rental> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Rental cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error updating rental: {ex.Message}");
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
                var entity = base._db.Rentals.FirstOrDefault(r => r.RentalID == id);
                if (entity != null) base._db.Rentals.Remove(entity);
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting rental: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Rental item)
        {
            BeginTransaction();
            try
            {
                var entity = base._db.Rentals.FirstOrDefault(r => r.RentalID == item.RentalID);
                if (entity != null) base._db.Rentals.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting rental: {ex.Message}");
            }
            return true;
        }
    }
}
