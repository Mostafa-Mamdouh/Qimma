using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository
{
    public class ServiceItemProfileReopsitory : GenericRepository<ent.Billing.ServiceItemProfile>, IServiceItemProfileReopsitory
    {
        public ServiceItemProfileReopsitory(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
