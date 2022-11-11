using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Basket.Models;

namespace RabbitMQ.Basket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        [HttpGet("basket")]
        public async Task<ActionResult> GetItemsAsync()
        {
            return Ok();
        }

        [HttpPost("basket")]
        public async Task<ActionResult> AddItem(BasketItem basketItem)
        {
            return Ok(basketItem);
        }

        [HttpPut("basket")]
        public async Task<ActionResult> UpdateItem(BasketItem basketItem)
        {
            return Ok(basketItem);
        }

        [HttpDelete("basket")]
        public async Task<ActionResult> DeleteItem(int itemId)
        {
            return Ok(itemId);
        }
    }
}
