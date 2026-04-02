using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DataGeneratorService
{
    internal interface IDataGeneratorServiceInterface<T>
    {
        List<T> GenerateData(int Amount);
    }
}
