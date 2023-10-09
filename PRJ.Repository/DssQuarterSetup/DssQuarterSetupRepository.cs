using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;
namespace PRJ.Repository.DssQuarterSetup
{
    public class DssQuarterSetupRepository : GenericRepository<ent.DssQuarterSetup>, IDssQuarterSetupRepository
    {
        public DssQuarterSetupRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
