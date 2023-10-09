using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    public class TransactionHeaderRepository : GenericRepository<ent.TransactionHeader>, ITransactionHeaderRepository
    {
        public TransactionHeaderRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }


}
