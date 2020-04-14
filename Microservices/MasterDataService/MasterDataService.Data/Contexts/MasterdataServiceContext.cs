using System.Linq;
using MasterDataService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterDataService.Data.Contexts
{
    public class MasterDataServiceContext : DbContext
    {
        public MasterDataServiceContext (DbContextOptions<MasterDataServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Cow> Cows { get; set; }

        public void CreateCow(Cow cow)
        {
            Cows.Add(cow);
            SaveChanges();
        }
        public void UpdateCow(Cow cow)
        {
            Cows.Update(cow);
            SaveChanges();
        }
        public void Upsert(Cow cow)
        {
            if (Cows.Contains(cow))
            {
                UpdateCow(cow);
            }
            else
            {
                CreateCow(cow);
            }
        }
    }
}
