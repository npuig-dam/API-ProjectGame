using GameAPI.Model;
using GameAPI.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [EnableCors]
    [Route("cgw/deck")] 
    [ApiController]
    public class DeckController : Controller
    {
        [HttpGet]
        public List<Deck> Get()
        {
            DeckService objDeckService = new DeckService();
            return objDeckService.GetAllDecks();
        }

        [HttpGet("{id}")]
        public Deck GetId(int id)
        {
            DeckService objDeckService = new DeckService();
            return objDeckService.GetById(id);
        }

        
        [HttpGet("search")]
        public Deck GetName([FromQuery] string name)
        {
            DeckService objDeckService = new DeckService();
            return objDeckService.GetByName(name);
        }

       
        [HttpPost]
        public int Insert([FromBody] Deck deck)
        {
            DeckService objDeckService = new DeckService();
            return objDeckService.Add(deck);
        }

     
        [HttpPut("{id}/cards")]
        public bool UpdateCards(int id, [FromBody] string newCardsIds)
        {
            DeckService objDeckService = new DeckService();
            return objDeckService.UpdateDeckCards(id, newCardsIds);
        }

        [HttpPut("{id}/name")]
        public bool UpdateName(int id, [FromBody] string newDeckName)
        {
            DeckService objDeckService = new DeckService();
            return objDeckService.UpdateDeckName(id, newDeckName);
        }
    }
}
