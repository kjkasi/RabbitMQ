using Microsoft.EntityFrameworkCore;

namespace RabbitMQ.Catalog.Models
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> opt) : base(opt)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CatalogItem>().HasData(
                new CatalogItem { Id = 1 },
                new CatalogItem { Id = 2 },
                new CatalogItem { Id = 3 }
            );
        }
    }
}
