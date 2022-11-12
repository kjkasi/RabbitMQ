using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Basket.Models;
using System.Net.Mime;

namespace RabbitMQ.Basket.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _itemRepo;

        public BasketController(IBasketRepository itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [HttpGet("basket")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> GetItems()
        {
            var items = await _itemRepo.GetBasketItems();
            return Ok(items);
        }

        [HttpGet("items/{id:int}", Name = "ItemById")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> ItemById(int id)
        {
            var item = await _itemRepo.GetBasketItem(id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        [HttpPost("basket")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> AddItem(BasketItem item)
        {
            await _itemRepo.CreateBasketItem(item);
            return CreatedAtAction(nameof(ItemById), new { Id = item.Id }, item);
        }

        [HttpPut("basket")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateItem(BasketItem item)
        {
            await _itemRepo.UpdateBasketItem(item);
            return CreatedAtAction(nameof(ItemById), new { Id = item.Id }, item);
        }

        [HttpDelete("basket")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var product = await _itemRepo.GetBasketItem(id);
            if (product == null)
            {
                return NotFound();
            }
            await _itemRepo.DeleteBasketItem(id);
            return NoContent();
        }
    }
}
