namespace RabbitMQ.Catalog.Models
{
    public interface ICatalogItemRepository
    {
        Task<IEnumerable<CatalogItem>> GetCatalogItems();
        Task<CatalogItem> GetCatalogItem(int itemId);
        Task<CatalogItem> CreateCatalogItem(CatalogItem item);
        Task<CatalogItem> UpdateCatalogItem(CatalogItem item);
        Task<bool> DeleteCatalogItem(int itemId);
    }
}
