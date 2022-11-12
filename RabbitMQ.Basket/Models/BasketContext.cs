using Microsoft.EntityFrameworkCore;

namespace RabbitMQ.Basket.Models
{
    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> opt) : base(opt)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BasketItem>().HasData(
                new BasketItem { Id = 1 },
                new BasketItem { Id = 2 },
                new BasketItem { Id = 3 }
            );
        }
    }
}
