using AutoMapper;
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
        private readonly IMapper _mapper;

        public CatalogController(ICatalogItemRepository itemRepo, IMapper mapper)
        {
            _itemRepo = itemRepo;
            _mapper = mapper;
        }

        [HttpGet("items")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<CatalogReadDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetItems()
        {
            var items = await _itemRepo.GetCatalogItems();
            var itemDtos = _mapper.Map<IEnumerable<CatalogReadDto>>(items);
            return Ok(itemDtos);
        }

        [HttpGet("items/{id:int}", Name = "ItemById")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CatalogReadDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> ItemById(int id)
        {
            var item = await _itemRepo.GetCatalogItem(id);
            if (item != null)
            {
                var itemDto = _mapper.Map<CatalogReadDto>(item);
                return Ok(itemDto);
            }
            return NotFound();
        }

        [HttpPost("items")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.Redirect)]
        public async Task<ActionResult> AddItem(CatalogCreateDto itemDto)
        {
            var item = _mapper.Map<CatalogItem>(itemDto);
            await _itemRepo.CreateCatalogItem(item);
            return CreatedAtAction(nameof(ItemById), new { item.Id }, item);
        }

        [HttpPut("items")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.Redirect)]
        public async Task<ActionResult> UpdateItem(CatalogReadDto itemDto)
        {
            var item = _mapper.Map<CatalogItem>(itemDto);
            await _itemRepo.UpdateCatalogItem(item);
            return CreatedAtAction(nameof(ItemById), new { Id = item.Id }, item);
        }

        [HttpDelete("items")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
