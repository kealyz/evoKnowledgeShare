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
            User result = myUserService.GetUserById(id);
            return result is not null ? Ok(result) : Problem(statusCode: StatusCodes.Status404NotFound, title: "No user found.");
        }

        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            User? result = await myUserService.CreateUserAsync(user);
            return result is not null ? Created(nameof(CreateUserAsync), result) : BadRequest("User cannot be added.");
        }

        [HttpDelete("Delete/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(Guid id)
        {
            try
            {
                myUserService.RemoveUserById(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("Update")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpdateUser([FromBody] User user)
        {

            User result = myUserService.Update(user);
            return result is not null ? Created(nameof(UpdateUser), result) : NotFound("User cannot be found.");
        }
    }
}
