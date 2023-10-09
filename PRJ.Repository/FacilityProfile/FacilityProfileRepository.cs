using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    public class FacilityProfileRepository : GenericRepository<ent.FacilityProfile>, IFacilityProfileRepository
    {
        public FacilityProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
