using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class InternalScreenRoleRepository : GenericRepository<ent.InternalScreenRole>, IInternalScreenRoleRepository
    {
        public InternalScreenRoleRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}