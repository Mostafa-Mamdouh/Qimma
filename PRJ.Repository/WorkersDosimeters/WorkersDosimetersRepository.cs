using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class WorkersDosimetersRepository : GenericRepository<ent.WorkersDosimeters>, IWorkersDosimetersRepository
    {
        public WorkersDosimetersRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
