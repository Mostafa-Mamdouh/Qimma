using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class LicenseProfileRepository : GenericRepository<ent.LicenseProfile>, ILicenseProfileRepository
    {
        public LicenseProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
