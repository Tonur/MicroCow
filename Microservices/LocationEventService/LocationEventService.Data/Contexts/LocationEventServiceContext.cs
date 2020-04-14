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

        public DbSet<CowLocation> CowLocations { get; set; }

        public void Create(CowLocation cowLocation)
        {
            CowLocations.Add(cowLocation);
            SaveChanges();
        }
        public void Update(CowLocation CowLocation)
        {
            CowLocations.Update(CowLocation);
            SaveChanges();
        }
        public void Upsert(CowLocation cowLocation)
        {
            if (CowLocations.Contains(cowLocation))
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
