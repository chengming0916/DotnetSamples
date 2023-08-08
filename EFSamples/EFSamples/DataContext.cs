using EFSamples.Configurations;
using System.Data.Entity;

namespace EFSamples
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("Name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
