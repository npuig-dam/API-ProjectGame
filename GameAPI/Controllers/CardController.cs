using GameAPI.Model;
using GameAPI.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    namespace GameAPI.Controllers
    {
        [EnableCors]
        [Route("cgw/card")] 
        [ApiController]
        public class CardController : Controller
        {

            [HttpGet("{id}")]
            public Card GetId(int id)
            {
                CardService objCardService = new CardService();
                return objCardService.GetCard(id);
            }

         
            [HttpGet("popular")]
            public Card GetMostPopular()
            {
                CardService objCardService = new CardService();
                return objCardService.GetMostPopularCard();
            }
        }
    }
}
