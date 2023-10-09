using ent = PRJ.Domain.Entities;
using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;


namespace PRJ.Repository.ServiceEntryFees
{
    public class ServiceEntryFeesRepository : GenericRepository<ent.BillingServiceTrn.ServiceEntryFees>, IServiceEntryFeesRepository
    {
        public ServiceEntryFeesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
