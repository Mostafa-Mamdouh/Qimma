using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class ManufacturerMasterRepository : GenericRepository<ent.ManufacturerMaster>, IManufacturerMasterRepository
    {
        public ManufacturerMasterRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
