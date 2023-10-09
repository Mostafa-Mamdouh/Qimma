using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class WorkFlowRejectReasonsRepository : GenericRepository<ent.WorkFlowRejectReasons>, IWorkFlowRejectReasonsRepository
    {
        public WorkFlowRejectReasonsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
