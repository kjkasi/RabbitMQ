using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Catalog.Models;
using System.Net.Mime;

namespace RabbitMQ.Catalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogItemRepository _itemRepo;

        public CatalogController(ICatalogItemRepository itemRepo)
        {
            _itemRepo = itemRepo;
        }

        [HttpGet("items")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> GetItems()
        {
            var items = await _itemRepo.GetCatalogItems();
            return Ok(items);
        }

        [HttpGet("items/{id:int}", Name = "ItemById")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> ItemById(int id)
        {
            var item = await _itemRepo.GetCatalogItem(id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }

        [HttpPost("items")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> AddItem(CatalogItem item)
        {
            await _itemRepo.CreateCatalogItem(item);
            return CreatedAtAction(nameof(ItemById), new { item.Id }, item);
        }

        [HttpPut("items")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> UpdateItem(CatalogItem item)
        {
            await _itemRepo.UpdateCatalogItem(item);
            return CreatedAtAction(nameof(ItemById), new { Id = item.Id }, item);
        }

        [HttpDelete("items")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var product = await _itemRepo.GetCatalogItem(id);
            if (product == null)
            {
                return NotFound();
            }
            await _itemRepo.DeleteCatalogItem(id);
            return NoContent();
        }
    }
}
