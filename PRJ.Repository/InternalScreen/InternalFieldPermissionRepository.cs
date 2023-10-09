using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
	public class InternalFieldPermissionRepository : GenericRepository<ent.InternalFieldPermission>, IInternalFieldPermissionRepository
    {
		public InternalFieldPermissionRepository(RepositoryContext db, ILogger logger) : base(db, logger)
		{

		}
	}
}