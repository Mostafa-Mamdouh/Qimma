using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    public class BillingServiceTrnHeaderRepository : GenericRepository<ent.BillingServiceTrn.BillingServiceTrnHeader>, IBillingServiceTrnHeaderRepository
    {
        public BillingServiceTrnHeaderRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
