namespace RabbitMQ.Basket.Models
{
    public interface IBasketRepository
    {
        Task<IEnumerable<BasketItem>> GetBasketItems();
        Task<BasketItem> GetBasketItem(int itemId);
        Task<BasketItem> CreateBasketItem(BasketItem item);
        Task<BasketItem> UpdateBasketItem(BasketItem item);
        Task<bool> DeleteBasketItem(int itemId);
    }
}
