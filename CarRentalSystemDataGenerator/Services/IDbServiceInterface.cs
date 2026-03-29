using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services
{
    internal interface IDbServiceInterface<T>
    {
        //Get
        T Get(int id);
        List<T> GetAll();
        T Get(T item);
        bool Exists(int id);

        //Create
        T Add(T item);
        int AddMany(List<T> items, bool CancelOnError);

        //Update
        T Update(T item);
        T Update(int id, T item);
        int UpdateMany(List<T> items, bool CancelOnError);

        //Delete
        bool Delete(int id);
        bool Delete(T item);

        //Transations
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
