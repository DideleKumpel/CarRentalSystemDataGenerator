using CarRentalSystemDataGenerator.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DbServices
{
    internal class DbServiceBase<T> : IDbServiceInterface<T>
    {
        public readonly AppDbContext _db;

        protected DbServiceBase(AppDbContext db)
        {
            _db = db;
        }

        public virtual void BeginTransaction()
        {
            _db.Database.BeginTransaction();
        }

        public virtual void CommitTransaction()
        {
            _db.Database.CommitTransaction();
        }
        public virtual void RollbackTransaction()
        {
            _db.Database.RollbackTransaction();
        }

        public virtual T Add(T item)
        {
            throw new NotImplementedException();
        }

        public virtual int AddMany(List<T> items, bool CancelOnError)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(T item)
        {
            throw new NotImplementedException();
        }

        public virtual bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(int id)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(T item)
        {
            throw new NotImplementedException();
        }

        public virtual List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        

        public virtual T Update(T item)
        {
            throw new NotImplementedException();
        }

        public virtual T Update(int id, T item)
        {
            throw new NotImplementedException();
        }

        public virtual int UpdateMany(List<T> items, bool CancelOnError)
        {
            throw new NotImplementedException();
        }
    }
}
