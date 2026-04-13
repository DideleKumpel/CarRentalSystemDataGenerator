using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class PaymentDbService : DbServiceBase<Payment>
    {
        public PaymentDbService(AppDbContext db) : base(db)
        {
        }

        public override Payment Get(int id)
        {
            var found = base._db.Payments.FirstOrDefault(p => p.PaymentID == id);
            if (found == null) throw new Exception($"Payment with id {id} not found.");
            return found;
        }

        public override List<Payment> GetAll()
        {
            var found = base._db.Payments.ToList();
            return found ?? new List<Payment>();
        }

        public override Payment Get(Payment item)
        {
            var found = base._db.Payments.FirstOrDefault(p => p.RentalID == item.RentalID && p.Amount == item.Amount && p.PaymentDate == item.PaymentDate);
            if (found == null) throw new Exception($"Payment not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Payments.Any(p => p.PaymentID == id);
        }

        public override Payment Add(Payment item)
        {
            if (item == null) throw new Exception("Payment cannot be null.");
            BeginTransaction();
            try
            {
                base._db.Payments.Add(item);
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error adding payment: {ex.Message}");
            }
            return item;
        }

        public override int AddMany(List<Payment> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Payment cannot be null.");
					base._db.Payments.Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error adding payment: {ex.Message}");
                    }
                }
            }
            CommitTransaction();
            return successCount;
        }

        public override Payment Update(Payment item)
        {
            if (item == null) throw new Exception("Payment cannot be null.");
            var found = base._db.Payments.FirstOrDefault(p => p.PaymentID == item.PaymentID);
            if (found == null) throw new Exception($"Payment with id {item.PaymentID} not found.");
            BeginTransaction();
            try
            {
                found.RentalID = item.RentalID;
                found.Amount = item.Amount;
                found.PaymentDate = item.PaymentDate;
                found.Method = item.Method;
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating payment: {ex.Message}");
            }
            return found;
        }

        public override Payment Update(int id, Payment item)
        {
            if (item == null) throw new Exception("Payment cannot be null.");
            var found = base._db.Payments.FirstOrDefault(p => p.PaymentID == id);
            if (found == null) throw new Exception($"Payment with id {id} not found.");
            BeginTransaction();
            try
            {
                found.RentalID = item.RentalID;
                found.Amount = item.Amount;
                found.PaymentDate = item.PaymentDate;
                found.Method = item.Method;
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating payment: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Payment> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Payment cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error updating payment: {ex.Message}");
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
                var entity = base._db.Payments.FirstOrDefault(p => p.PaymentID == id);
                if (entity != null) base._db.Payments.Remove(entity);
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting payment: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Payment item)
        {
            BeginTransaction();
            try
            {
                var entity = base._db.Payments.FirstOrDefault(p => p.PaymentID == item.PaymentID);
                if (entity != null) base._db.Payments.Remove(entity);
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting payment: {ex.Message}");
            }
            return true;
        }
    }
}
