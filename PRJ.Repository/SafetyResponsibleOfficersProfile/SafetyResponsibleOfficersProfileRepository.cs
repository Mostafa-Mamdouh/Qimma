using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class SafetyResponsibleOfficersProfileRepository : GenericRepository<ent.SafetyResponsibleOfficersProfile>, ISafetyResponsibleOfficersProfileRepository
    {
        public SafetyResponsibleOfficersProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
