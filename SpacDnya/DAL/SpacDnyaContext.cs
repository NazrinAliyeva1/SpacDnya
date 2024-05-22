using Microsoft.EntityFrameworkCore;
using SpacDnya.Models;

namespace SpacDnya.DAL
{
    public class SpacDnyaContext:DbContext
    {

        public SpacDnyaContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Agency> Agencies { get; set; }
    }
}
