using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace evoKnowledgeShare.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private HistoryService myHistoryService;

        public HistoryController(HistoryService myHistoryService)
        {
            this.myHistoryService = myHistoryService;
        }

        /// <summary>
        /// Get every history from the database
        /// </summary>
        /// <returns>Returns every <see cref="History"/> entity from the database</returns>
        /// <exception cref="System.Exception">Unexpected exception</exception>
        [HttpGet("")]
        public IActionResult GetHistories()
        {
            try
            {
                var notes = myHistoryService.GetAll();
                if (notes.Any())
                {
                    return Ok(notes);
                }
                else return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get history by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="History"/> entity with the matching id</returns>
        /// <exception cref="System.InvalidOperationException">A method call is invalid for the object's current state</exception>
        /// <exception cref="System.ArgumentNullException">Argument is null</exception>
        [HttpGet("{id}")]
        public IActionResult GetHistoryById(Guid id)
        {
            try
            {
                return Ok(myHistoryService.GetById(id));
            }
            catch(KeyNotFoundException) 
            {
                return NotFound();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
        }

        /// <summary>
        /// Create new history entity
        /// </summary>
        /// <param name="history"></param>
        /// <returns>Create a <see cref="History"/> enity</returns>
        /// <exception cref="System.ArgumentException">Not valid arguments exception</exception>
        /// <exception cref="System.Exception">Unexpected exception</exception>
        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateHistoryAsync([FromBody] HistoryDTO history)
        {
            try
            {
                var result = await myHistoryService.CreateHistory(new History(history));
                return Created(nameof(CreateHistoryAsync), result);
            }
            catch (ArgumentException)
            {
                return BadRequest("History cannot be added.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
