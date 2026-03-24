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
        public IActionResult Get()
        {
            try
            {
                // This is your existing logic
                LoginService objLoginService = new LoginService();
                var logins = objLoginService.GetAllLogins();
                return Ok(logins);
            }
            catch (System.Exception ex)
            {
                // This will print the EXACT error (like "Access Denied" or "Host Unknown") 
                // directly in your browser window instead of just saying "500"
                return StatusCode(500, $"Internal Error: {ex.Message} | StackTrace: {ex.StackTrace}");
            }
        }

        /*[HttpGet]
        public List<Login> Get()
        {
            LoginService objLoginService = new LoginService();
            return objLoginService.GetAllLogins();
        }*/

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

        [HttpGet]
        public bool PasswdVer(int Id, string Passwd)
        {
            LoginService objLoginService = new LoginService();
            return objLoginService.PasswdCorrect(Id, Passwd);
        }
    }
}
