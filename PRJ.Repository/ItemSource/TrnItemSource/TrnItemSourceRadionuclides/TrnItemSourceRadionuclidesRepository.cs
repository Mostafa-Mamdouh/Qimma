using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class TrnItemSourceRadionuclidesRepository : GenericRepository<ent.TrnItemSourceRadionuclides>, ITrnItemSourceRadionuclidesRepository
    {
        public TrnItemSourceRadionuclidesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
