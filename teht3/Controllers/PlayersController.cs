using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace teht3.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IRepository _repository;

        public PlayersController(ILogger<PlayersController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("{playerId}")]
        public Task<Player> Get(Guid id)
        {
            return _repository.Get(id);
        }

        [HttpGet]
        public Task<Player[]> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpPost]
        public async Task<Player> Create([FromBody] NewPlayer newplayer)
        {
            Player _player = new Player();
            _player.Id = Guid.NewGuid();
            _player.Name = newplayer.Name;

            await _repository.Create(_player);
            return _player;
        }

        [HttpPost]
        public Task<Player> Modify(Guid id, [FromBody] ModifiedPlayer player)
        {
            return _repository.Modify(id, player);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task<Player> Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
