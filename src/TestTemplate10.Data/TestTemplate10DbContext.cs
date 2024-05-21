using MassTransit;
using Microsoft.EntityFrameworkCore;
using TestTemplate10.Core.Entities;

namespace TestTemplate10.Data
{
    public class TestTemplate10DbContext : DbContext
    {
        public TestTemplate10DbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Foo> Foos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
        }
    }
}
