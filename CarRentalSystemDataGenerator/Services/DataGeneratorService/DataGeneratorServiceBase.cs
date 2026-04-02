using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal class DataGeneratorServiceBase<T> : IDataGeneratorServiceInterface<T>
    {
        protected Random Random;

        public DataGeneratorServiceBase()
        {
            this.Random = new Random();
        }

        public DataGeneratorServiceBase(int seed)
        {
            this.Random = new Random(seed);
        }

        public List<T> GenerateData(int Amount)
        {
            throw new NotImplementedException();
        }
    }
}
