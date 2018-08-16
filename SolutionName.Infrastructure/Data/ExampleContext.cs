using Microsoft.EntityFrameworkCore;
using SolutionName.Core.Entities;

namespace SolutionName.Infrastructure.Data
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options) : base(options)
        {

        }

        public DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Example>().ToTable("Example");
        }
    }
}
