using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class TrnItemSourcesFilesRepository : GenericRepository<TransactionAttachments>, ITrnItemSourceFilesRepository
    {
        public TrnItemSourcesFilesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
