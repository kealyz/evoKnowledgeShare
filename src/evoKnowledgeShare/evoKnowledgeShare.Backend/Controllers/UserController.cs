using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace evoKnowledgeShare.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService myUserService;

        public UserController(UserService myUserService)
        {
            this.myUserService = myUserService;
        }

        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            return Ok(myUserService.Get());
        }
    }
}
