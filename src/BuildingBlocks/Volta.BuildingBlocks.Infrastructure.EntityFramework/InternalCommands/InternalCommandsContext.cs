using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Volta.BuildingBlocks.Infrastructure.EntityFramework.InternalCommands
{
    public class InternalCommandsContext : DbContext
    {
        public InternalCommandsContext(DbContextOptions<InternalCommandsContext> options) : base(options)
        {
        }

        public DbSet<InternalCommandLogEntry> InternalCommandLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InternalCommandLogEntry>(ConfigureInternalCommandLogEntry);
        }

        void ConfigureInternalCommandLogEntry(EntityTypeBuilder<InternalCommandLogEntry> builder)
        {
            builder.ToTable("InternalCommandLogEntry");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.Data)
                .IsRequired();

            builder.Property(e => e.EnqueueDate)
                .IsRequired();

            builder.Property(e => e.Type)
                .IsRequired();
        }

        public class IntegrationEventLogContextDesignFactory : IDesignTimeDbContextFactory<InternalCommandsContext>
        {
            public InternalCommandsContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<InternalCommandsContext>()
                    .UseSqlServer("Server=.;Initial Catalog=InternalCommandsDb;Integrated Security=true");

                return new InternalCommandsContext(optionsBuilder.Options);
            }
        }
    }
}