using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using evoKnowledgeShare.Backend.DTO;
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

        #region Get Section

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsers()
        {
            try
            {
                return myUserService.GetAll().Count() > 0 ? Ok(myUserService.GetAll()) : NotFound(myUserService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                User result = myUserService.GetUserById(id);
                return Ok(result);
            }
            catch(ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch(KeyNotFoundException)
            {
                return NotFound("User cannot be found.");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("ByUserName/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserByUserName(string username)
        {
            try
            {
                User? result = myUserService.GetUserByUserName(username);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (KeyNotFoundException)
            {
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion Get Section

        #region Post Section

        [HttpPost("UserRange/{ids}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserRangeById([FromBody] IEnumerable<Guid> ids)
        {
            try 
            {
                IEnumerable<User> results = myUserService.GetUserRangeById(ids);
                return Ok(results);
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Users cannot be found.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDTO user)
        {
            try 
            {
                User? result = await myUserService.CreateUserAsync(new User(user));
                return Created(nameof(CreateUserAsync),result);
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (OperationCanceledException)
            {
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion Post Section

        #region Delete Section

        [HttpDelete("")]
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
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User cannot be found.");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
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
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User cannot be found.");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        #endregion Delete Section

        #region Put Section

        [HttpPut("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser([FromBody] User user)
        {
            try
            {
                User result = myUserService.Update(user);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User cannot be found.");
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateRange")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateRange([FromBody] IEnumerable<User> users)
        {       
            try
            {
                IEnumerable<User> result = myUserService.UpdateRange(users);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Users cannot be found.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion Put Section
    }
}
