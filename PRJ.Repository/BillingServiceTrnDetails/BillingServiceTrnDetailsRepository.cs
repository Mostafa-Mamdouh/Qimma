using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    public class BillingServiceTrnDetailsRepository : GenericRepository<ent.BillingServiceTrn.BillingServiceTrnDetails>, IBillingServiceTrnDetailsRepository
    {
        public BillingServiceTrnDetailsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
