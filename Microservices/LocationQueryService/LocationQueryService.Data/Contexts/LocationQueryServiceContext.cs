using LocationQueryService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LocationQueryService.Data.Contexts
{
    public class LocationQueryServiceContext : DbContext
    {
        public LocationQueryServiceContext (DbContextOptions<LocationQueryServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        

    }
}
