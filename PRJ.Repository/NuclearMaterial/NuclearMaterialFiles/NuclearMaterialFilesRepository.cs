using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class NuclearMaterialFilesRepository : GenericRepository<ent.NuclearMaterial.NuclearMaterialFiles>, INuclearMaterialFilesRepository
    {
        public NuclearMaterialFilesRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
