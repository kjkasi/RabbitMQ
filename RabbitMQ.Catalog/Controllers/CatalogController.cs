using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Catalog.Models;
using System.Net;
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
        [ProducesResponseType(typeof(IEnumerable<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetItems()
        {
            var items = await _itemRepo.GetCatalogItems();
            return Ok(items);
        }

        [HttpGet("items/{id:int}", Name = "ItemById")]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.NotFound)]
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
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.Redirect)]
        public async Task<ActionResult> AddItem(CatalogItem item)
        {
            await _itemRepo.CreateCatalogItem(item);
            return CreatedAtAction(nameof(ItemById), new { item.Id }, item);
        }

        [HttpPut("items")]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.Redirect)]
        public async Task<ActionResult> UpdateItem(CatalogItem item)
        {
            await _itemRepo.UpdateCatalogItem(item);
            return CreatedAtAction(nameof(ItemById), new { Id = item.Id }, item);
        }

        [HttpDelete("items")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.NotFound)]
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
