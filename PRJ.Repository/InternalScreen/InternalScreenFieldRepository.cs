using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
	public class InternalScreenFieldRepository : GenericRepository<ent.InternalScreenField>, IInternalScreenFieldRepository
	{
		public InternalScreenFieldRepository(RepositoryContext db, ILogger logger) : base(db, logger)
		{

		}
	}
}