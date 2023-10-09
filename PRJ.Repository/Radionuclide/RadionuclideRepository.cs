﻿using Microsoft.Extensions.Logging;
using PRJ.DataAccess.MSSQL;
using ent = PRJ.Domain.Entities;

namespace PRJ.Repository
{
    internal class RadionuclideRepository : GenericRepository<ent.Radionuclides>, IRadionuclideRepository
    {
        public RadionuclideRepository(RepositoryContext db, ILogger logger) : base(db, logger)
        {

        }
    }
}
