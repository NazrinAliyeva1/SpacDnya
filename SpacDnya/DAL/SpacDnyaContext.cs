using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpacDnya.Models;

namespace SpacDnya.DAL
{
    public class SpacDnyaContext:IdentityDbContext<AppUser>
    {

        public SpacDnyaContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AppUser>AppUsers { get; set; }
    }
}
