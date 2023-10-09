using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;

using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class RelatedItemHierarchyRepository : GenericRepository<ent.RelatedItemsHierarchyStructure>, IRelatedItemHierarchyRepository
    {
        public RelatedItemHierarchyRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
