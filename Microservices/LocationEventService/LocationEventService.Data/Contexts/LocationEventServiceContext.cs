using System.Linq;
using LocationEventService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LocationEventService.Data.Contexts
{
    public class LocationEventServiceContext : DbContext
    {
        public LocationEventServiceContext (DbContextOptions<LocationEventServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        public void Create(Location cowLocation)
        {
            Locations.Add(cowLocation);
            SaveChanges();
        }
        public void Update(Location CowLocation)
        {
            Locations.Update(CowLocation);
            SaveChanges();
        }
        public void Upsert(Location cowLocation)
        {
            if (Locations.Contains(cowLocation))
            {
                Update(cowLocation);
            }
            else
            {
                Create(cowLocation);
            }
        }
    }
}
