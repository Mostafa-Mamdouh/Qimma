using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using PRJ.Repository.DssQuarterSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository.DssFiscalYears
{
    public class DssFiscalYearsRepository : GenericRepository<ent.DssFiscalYears>, IDssFiscalYearsRepository
    {
        public DssFiscalYearsRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
