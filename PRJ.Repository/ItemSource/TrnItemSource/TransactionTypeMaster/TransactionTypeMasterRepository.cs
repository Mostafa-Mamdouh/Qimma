using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class TransactionTypesMasterRepository : GenericRepository<ent.TransactionTypeMaster>, ITransactionTypeMasterRepository
    {
        public TransactionTypesMasterRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
