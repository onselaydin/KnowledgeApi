using KnowledgeApi.Models;
using Microsoft.AspNetCore.Mvc;
using KnowledgeApi.Services;

namespace KnowledgeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticateService _authenticateService;
        public AuthenticationController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

      

        [HttpPost]
        public IActionResult Post([FromBody]User model)
        {
            var check = _authenticateService.CheckUser(model.UserName);
            if (!check)
                _authenticateService.CreateUser(model);
            var user = _authenticateService.Authenticate(model.UserName, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or Password is incorrect" });
            return Ok(user);

        }
        
    }
}
