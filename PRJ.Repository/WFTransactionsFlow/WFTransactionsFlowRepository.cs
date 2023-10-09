using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class WFTransactionsFlowRepository : GenericRepository<ent.WFTransactionsFlow>, IWFTransactionsFlowRepository
    {
        public WFTransactionsFlowRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
