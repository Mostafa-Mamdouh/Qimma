using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;


namespace PRJ.Repository
{
    public class LegalRepresentativesProfileRepository : GenericRepository<ent.LegalRepresentativesProfile>, ILegalRepresentativesProfileRepository
    {
        public LegalRepresentativesProfileRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
