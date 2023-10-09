using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class WorkersExposuresDosesRepository : GenericRepository<ent.WorkersExposuresDoses>, IWorkersExposuresDosesRepository
    {
        public WorkersExposuresDosesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
