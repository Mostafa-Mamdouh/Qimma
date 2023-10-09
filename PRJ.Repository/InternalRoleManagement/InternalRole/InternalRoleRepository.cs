using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class InternalRoleRepository : GenericRepository<ent.InternalRole>, IInternalRoleRepository
    {
        public InternalRoleRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}