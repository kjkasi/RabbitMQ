using Microsoft.EntityFrameworkCore;

namespace RabbitMQ.Catalog.Models
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly CatalogContext _context;

        public CatalogItemRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<CatalogItem> CreateCatalogItem(CatalogItem item)
        {
            _context.CatalogItems.Add(item);

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteCatalogItem(int itemId)
        {
            CatalogItem item = await _context.CatalogItems.FirstOrDefaultAsync(u => u.Id == itemId);
            if (item == null)
            {
                return false;
            }
            _context.CatalogItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CatalogItem> GetCatalogItem(int itemId)
        {
            CatalogItem item = await _context.CatalogItems.Where(x => x.Id == itemId).FirstOrDefaultAsync();
            return item;
        }

        public async Task<IEnumerable<CatalogItem>> GetCatalogItems()
        {
            List<CatalogItem> itemList = await _context.CatalogItems.ToListAsync();
            return itemList;
        }

        public async Task<CatalogItem> UpdateCatalogItem(CatalogItem item)
        {
            _context.CatalogItems.Update(item);

            await _context.SaveChangesAsync();
            return item;
        }
    }
}
