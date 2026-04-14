using CarRentalSystemDataGenerator.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRentalSystemDataGenerator.Services.DbExportService
{
    internal class DbExportServiceBase : IDbExportServiceInterface
    {
        public readonly AppDbContext _db;

        protected DbExportServiceBase(AppDbContext db)
        {
            _db = db;
        }
        public virtual string ExportToXml()
        {
            throw new NotImplementedException();
        }

        public virtual string ExportToJSON()
        {
            throw new NotImplementedException();
        }
    }
}
