using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class OfficeDbService : DbServiceBase<Office>
    {
        public OfficeDbService(AppDbContext db) : base(db)
        {
        }

        public override Office Get(int id)
        {
            var found = base._db.Offices.FirstOrDefault(o => o.OfficeID == id);
            if (found == null) throw new Exception($"Office with id {id} not found.");
            return found;
        }

        public override List<Office> GetAll()
        {
            var found = base._db.Offices.ToList();
            return found ?? new List<Office>();
        }

        public override Office Get(Office item)
        {
            var found = base._db.Offices.FirstOrDefault(o => o.OfficeName == item.OfficeName && o.AddressID == item.AddressID);
            if (found == null) throw new Exception($"Office {item.OfficeName} not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Offices.Any(o => o.OfficeID == id);
        }

        public override Office Add(Office item)
        {
            if (item == null) throw new Exception("Office cannot be null.");
            base.BeginTransaction();
            try
            {
                base._db.Offices.Add(item);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error adding office: {ex.Message}");
            }
            return item;
        }

        public override int AddMany(List<Office> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Office cannot be null.");
                    Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error adding office: {ex.Message}");
                    }
                }
            }
            base._db.SaveChanges();
            return successCount;
        }

        public override Office Update(Office item)
        {
            if (item == null) throw new Exception("Office cannot be null.");
            var found = base._db.Offices.FirstOrDefault(o => o.OfficeID == item.OfficeID);
            if (found == null) throw new Exception($"Office with id {item.OfficeID} not found.");
            base.BeginTransaction();
            try
            {
                found.AddressID = item.AddressID;
                found.OfficeName = item.OfficeName;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating office: {ex.Message}");
            }
            return found;
        }

        public override Office Update(int id, Office item)
        {
            if (item == null) throw new Exception("Office cannot be null.");
            var found = base._db.Offices.FirstOrDefault(o => o.OfficeID == id);
            if (found == null) throw new Exception($"Office with id {id} not found.");
            base.BeginTransaction();
            try
            {
                found.AddressID = item.AddressID;
                found.OfficeName = item.OfficeName;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error updating office: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Office> items, bool CancelOnError)
        {
            base.BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Office cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        base.RollbackTransaction();
                        throw new Exception($"Error updating office: {ex.Message}");
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
                var entity = base._db.Offices.FirstOrDefault(o => o.OfficeID == id);
                if (entity != null) base._db.Offices.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting office: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Office item)
        {
            base.BeginTransaction();
            try
            {
                var entity = base._db.Offices.FirstOrDefault(o => o.OfficeID == item.OfficeID);
                if (entity != null) base._db.Offices.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                base.RollbackTransaction();
                throw new Exception($"Error deleting office: {ex.Message}");
            }
            return true;
        }
    }
}
