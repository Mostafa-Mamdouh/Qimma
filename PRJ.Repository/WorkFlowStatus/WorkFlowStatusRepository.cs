using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class WorkFlowStatusRepository : GenericRepository<ent.WorkFlowStatus>, IWorkFlowStatusRepository
    {
        public WorkFlowStatusRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
