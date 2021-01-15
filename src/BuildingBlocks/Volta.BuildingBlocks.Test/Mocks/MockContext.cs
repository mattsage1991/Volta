using Microsoft.EntityFrameworkCore;

namespace Volta.BuildingBlocks.Test.Mocks
{
    public class MockContext : DbContext
    {
        public DbSet<MockEntity> MockEntities { get; set; }
        public MockContext()
        {

        }

        public MockContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MockEntityConfiguration());
        }
    }
}
