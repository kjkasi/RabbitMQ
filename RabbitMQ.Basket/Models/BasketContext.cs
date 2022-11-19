using Microsoft.EntityFrameworkCore;

namespace RabbitMQ.Basket.Models
{
    public class BasketContext : DbContext
    {
        public BasketContext(DbContextOptions<BasketContext> opt) : base(opt)
        {

        }

        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BasketItem>().HasData(
                new BasketItem { Id = 1, Name = "name1", Description = "desc1" },
                new BasketItem { Id = 2, Name = "name2", Description = "desc2" },
                new BasketItem { Id = 3, Name = "name3", Description = "desc3" }
            );
        }
    }
}
