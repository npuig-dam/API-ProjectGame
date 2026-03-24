using GameAPI.Model;
using GameAPI.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [EnableCors]
    [Route("cgw/player")]
    [ApiController]

    public class PlayerController : Controller
    {
     
        [HttpGet("{id}")]
        public Player Get(int id)
        {
            PlayerService objPlayerService = new PlayerService();
            return objPlayerService.GetPlayer(id);
        }


      
        [HttpPost]
        public int Insert([FromBody] Player player)
        {
            PlayerService objPlayerService = new PlayerService();
            return objPlayerService.Add(player);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Player player)
        {
            var service = new PlayerService();
            var existing = service.GetPlayer(id);

            if (existing == null)
                return NotFound();

            existing.Deck = player.Deck ?? existing.Deck;
           

            service.Update(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PlayerService objPlayerService = new PlayerService();
            objPlayerService.Delete(id);
        }

    }
}
