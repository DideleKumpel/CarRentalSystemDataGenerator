using CarRentalSystemDataGenerator.DB;
using CarRentalSystemDataGenerator.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class ModelDbService : DbServiceBase<Model>
    {
        public ModelDbService(AppDbContext db) : base(db)
        {
        }

        public override Model Get(int id)
        {
            var found = base._db.Models.FirstOrDefault(m => m.ModelID == id);
            if (found == null) throw new Exception($"Model with id {id} not found.");
            return found;
        }

        public override List<Model> GetAll()
        {
            var found = base._db.Models.ToList();
            return found ?? new List<Model>();
        }

        public override Model Get(Model item)
        {
            var found = base._db.Models.FirstOrDefault(m => m.Name == item.Name && m.BrandID == item.BrandID);
            if (found == null) throw new Exception($"Model {item.Name} not found.");
            return found;
        }

        public override bool Exists(int id)
        {
            return base._db.Models.Any(m => m.ModelID == id);
        }

        public override Model Add(Model item)
        {
            if (item == null) throw new Exception("Model cannot be null.");
            BeginTransaction();
            try
            {
                base._db.Models.Add(item);
				CommitTransaction();
			}
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error adding model: {ex.Message}");
            }
            return item;
        }

        public override int AddMany(List<Model> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Model cannot be null.");
                    base._db.Models.Add(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error adding model: {ex.Message}");
                    }
                }
            }
			CommitTransaction();
			return successCount;
        }

        public override Model Update(Model item)
        {
            if (item == null) throw new Exception("Model cannot be null.");
            var found = base._db.Models.FirstOrDefault(m => m.ModelID == item.ModelID);
            if (found == null) throw new Exception($"Model with id {item.ModelID} not found.");
            BeginTransaction();
            try
            {
                found.BrandID = item.BrandID;
                found.Name = item.Name;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating model: {ex.Message}");
            }
            return found;
        }

        public override Model Update(int id, Model item)
        {
            if (item == null) throw new Exception("Model cannot be null.");
            var found = base._db.Models.FirstOrDefault(m => m.ModelID == id);
            if (found == null) throw new Exception($"Model with id {id} not found.");
            BeginTransaction();
            try
            {
                found.BrandID = item.BrandID;
                found.Name = item.Name;
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error updating model: {ex.Message}");
            }
            return found;
        }

        public override int UpdateMany(List<Model> items, bool CancelOnError)
        {
            BeginTransaction();
            int successCount = 0;
            foreach (var item in items)
            {
                try
                {
                    if (item == null) throw new Exception("Model cannot be null.");
                    Update(item);
                    successCount++;
                }
                catch (Exception ex)
                {
                    if (CancelOnError)
                    {
                        RollbackTransaction();
                        throw new Exception($"Error updating model: {ex.Message}");
                    }
                }
            }
            base._db.SaveChanges();
            return successCount;
        }

        public override bool Delete(int id)
        {
            BeginTransaction();
            try
            {
                var entity = base._db.Models.FirstOrDefault(m => m.ModelID == id);
                if (entity != null) base._db.Models.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting model: {ex.Message}");
            }
            return true;
        }

        public override bool Delete(Model item)
        {
            BeginTransaction();
            try
            {
                var entity = base._db.Models.FirstOrDefault(m => m.ModelID == item.ModelID);
                if (entity != null) base._db.Models.Remove(entity);
                base._db.SaveChanges();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw new Exception($"Error deleting model: {ex.Message}");
            }
            return true;
        }
    }
}
