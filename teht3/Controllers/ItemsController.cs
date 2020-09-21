using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace teht3.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IRepository _repository;

        public ItemsController(ILogger<ItemsController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("{playerId:Guid}/{itemId:Guid}")]
        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            return await _repository.GetItem(playerId, itemId);
        }

        [HttpGet]
        [Route("{playerId:Guid}")]

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            return await _repository.GetAllItems(playerId);
        }

        [HttpPost]
        [Route("{playerId:Guid}")]
        public async Task<Item> CreateItem(Guid playerId, [FromBody] NewItem newItem)
        {
            Item _item = new Item();
            _item.itemId = Guid.NewGuid();
            _item.level = newItem.level;
            _item.type = newItem.type;

            return await _repository.CreateItem(playerId, _item);
        }

        [HttpPost]
        [Route("modify/{playerId:Guid}/{itemId:Guid}")]
        public async Task<Item> UpdateItem(Guid playerId, Guid itemId, [FromBody] ModifiedItem item)
        {
            return await _repository.UpdateItem(playerId, itemId, item);
        }

        [HttpDelete]
        [Route("{playerId:Guid}/{itemId:Guid}")]
        public async Task<Item> DeleteItem(Guid playerId, Guid itemId)
        {
            return await _repository.DeleteItem(playerId, itemId);
        }
    }
}
