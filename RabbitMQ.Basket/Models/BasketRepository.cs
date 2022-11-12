using Microsoft.EntityFrameworkCore;

namespace RabbitMQ.Basket.Models
{
    public class BasketRepository : IBasketRepository
    {
        private readonly BasketContext _context;

        public BasketRepository(BasketContext context) 
        {
            _context = context;
        }

        public async Task<BasketItem> CreateBasketItem(BasketItem item)
        {
            _context.BasketItems.Add(item);

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteBasketItem(int itemId)
        {
            BasketItem item = await _context.BasketItems.FirstOrDefaultAsync(u => u.Id == itemId);
            if (item == null)
            {
                return false;
            }
            _context.BasketItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<BasketItem> GetBasketItem(int itemId)
        {
            BasketItem item = await _context.BasketItems.Where(x => x.Id == itemId).FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<BasketItem>> GetBasketItems()
        {
            List<BasketItem> itemList = await _context.BasketItems.ToListAsync();
            return itemList;
        }

        public async Task<BasketItem> UpdateBasketItem(BasketItem item)
        {
            _context.BasketItems.Update(item);

            await _context.SaveChangesAsync();
            return item;
        }
    }
}
