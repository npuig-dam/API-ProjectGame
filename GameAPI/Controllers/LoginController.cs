using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using GameAPI.Model;
using GameAPI.Service;

namespace GameAPI.Controllers
{
    [EnableCors]
    [Route("cgw/login")]
    [ApiController]
    public class LoginController : Controller
    {

        [HttpGet]
        public List<Login> Get()
        {
            LoginService objLoginService = new LoginService();
            return objLoginService.GetAllLogins();
        }

        [HttpGet("{id}")]
        public Login GetId(int id)
        {
            LoginService objLoginService = new LoginService();
            return objLoginService.GetById(id);
        }

        [HttpPost]
        public int Insert([FromBody] Login login)
        {
            LoginService objLoginService = new LoginService();
            return objLoginService.Add(login);
        }

        [HttpGet("verify")]
        public bool PasswdVer(int Id, string Passwd)
        {
            LoginService objLoginService = new LoginService();
            return objLoginService.PasswdCorrect(Id, Passwd);
        }
    }
}
