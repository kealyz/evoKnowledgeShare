using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

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
            IEnumerable<User> result = myUserService.Get();
            return result.Any() ? Ok(result) : Problem(statusCode: StatusCodes.Status404NotFound, title: "No users found.");
        }

        [HttpGet("User/{id}")]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(myUserService.GetUserById(id));
        }
        
        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            User? result = await myUserService.CreateUserAsync(user);
            return result is not null ? Created(nameof(CreateUserAsync),result) : BadRequest("User cannot be added.");
        }

        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(Guid id)
        {
            User result = myUserService.RemoveUserById(id);
            return result is not null ? Created(nameof(CreateUserAsync), result) : BadRequest("User cannot be added.");
        }
    }
}
