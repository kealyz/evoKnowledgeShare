using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using evoKnowledgeShare.Backend.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Collections.Generic;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            IEnumerable<User> result = myUserService.Get();
            return result.Any() ? Ok(result) : Problem(statusCode: StatusCodes.Status404NotFound, title: "No users found.");
        }

        [HttpGet("User/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                User result = myUserService.GetUserById(id);
                return Ok(result);
            }
            catch(KeyNotFoundException)
            {
                return NotFound();
            }

        }

        [HttpPost("UserRange/{ids}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserRangeById([FromBody] IEnumerable<Guid> ids)
        {
            IEnumerable<User> result = myUserService.GetUserRangeById(ids);
            //return result is not null ? Ok(result) : NotFound();
            return Ok(result);
        }

        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDTO user)
        {
            User? result = await myUserService.CreateUserAsync(new User(user));
            return result is not null ? Created(nameof(CreateUserAsync), result) : BadRequest("User cannot be added.");
        }

        [HttpDelete("Delete")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser([FromBody] User user)
        {
            try
            {
                myUserService.Remove(user);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUserById(Guid id)
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
                return StatusCode(500);
            }
        }

        [HttpPut("Update")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser([FromBody] UserDTO user)
        {

            User result = myUserService.Update(new User(user));
            return result is not null ? Created(nameof(UpdateUser), result) : NotFound("User cannot be found.");
        }

        [HttpPut("UpdateRange")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRange([FromBody] IEnumerable<User> users)
        {
            IEnumerable<User> result = myUserService.UpdateRange(users);
            return result is not null ? Created(nameof(UpdateRange), result) : NotFound("Users cannot be found.");
        }
    }
}
