using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PRJ_ID4Server.DataContext
{
    public class IdentityServerDbContext : IdentityDbContext
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
        {
        }
    }
}
