using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Catalog.Models;

namespace RabbitMQ.Catalog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        [HttpGet("items")]
        public async Task<ActionResult> GetItemsAsync()
        {
            return Ok();
        }

        [HttpPost("items")]
        public async Task<ActionResult> AddItem(CatalogItem catalogItem)
        {
            return Ok(catalogItem);
        }

        [HttpPut("items")]
        public async Task<ActionResult> UpdateItem(CatalogItem catalogItem)
        {
            return Ok(catalogItem);
        }

        [HttpDelete("items")]
        public async Task<ActionResult> DeleteItem(int itemId)
        {
            return Ok(itemId);
        }
    }
}
