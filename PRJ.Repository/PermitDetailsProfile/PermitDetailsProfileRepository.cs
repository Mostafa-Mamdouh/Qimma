using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class PermitDetailsProfileRepository : GenericRepository<ent.PermitDetailsProfile>, IPermitDetailsProfileRepository
    {
        public PermitDetailsProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
