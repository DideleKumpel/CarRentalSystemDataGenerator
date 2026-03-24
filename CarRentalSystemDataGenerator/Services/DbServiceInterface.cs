using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services
{
    internal interface DbServiceInterface<T>
    {
        //Get
        T Get(int id);
        List<T> GetAll();
        List<T> GetByFilter(Func<T, bool> filter);
        T Get(T item);
        bool Exists(int id);
        bool Exclude(T item);

        //Create
        T Add(T item);
        int AddMany(List<T> items);

        //Update
        T Update(T item);
        T Update(int id, T item);
        T UpdateMany(List<T> items);

        //Delete
        bool Delete(int id);
        bool Delete(T item);

        //Transations
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
