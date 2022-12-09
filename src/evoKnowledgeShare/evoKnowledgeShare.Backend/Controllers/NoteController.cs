using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace evoKnowledgeShare.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private NoteService myNoteService;

        public NoteController(NoteService myNoteService)
        {
            this.myNoteService = myNoteService;
        }

        #region Get Section
        [HttpGet("all")]
        public IActionResult GetNotes()
        {
            try
            {
                return Ok(myNoteService.GetAll());
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

        [HttpGet("{noteId}")]
        public IActionResult GetById(Guid noteId)
        {
            try
            {
                return Ok(myNoteService.GetById(noteId));
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
        [HttpGet("byUserId/{userId}")]
        public IActionResult GetByUserId(Guid userId)
        {
            try
            {
                return Ok(myNoteService.GetRangeByUserId(userId));
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
        [HttpGet("byTopicId/{topicId}")]
        public IActionResult GetByTopicId(int topicId)
        {
            try
            {
                return Ok(myNoteService.GetByTopicId(topicId));
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
        [HttpGet("byDescription/{description}")]
        public IActionResult GetByDescription(string description)
        {
            try
            {
                return Ok(myNoteService.GetByDescription(description));
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
        [HttpGet("byTitle/{title}")]
        public IActionResult GetByTitle(string title)
        {
            try
            {
                return Ok(myNoteService.GetByTitle(title));
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

        #region Add Section
        [HttpPost("create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] Note note)
        {
            Note? result = await myNoteService.AddAsync(note);
            return result is not null ? Created(nameof(AddAsync), result) : BadRequest("Note cannot be added.");
        }
        [HttpPost("createRange")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRangeAsync([FromBody] IEnumerable<Note> notes)
        {
            IEnumerable<Note> result = await myNoteService.AddRangeAsync(notes);
            return result is not null ? Created(nameof(AddRangeAsync), result) : BadRequest("Note cannot be added.");
        }

        #endregion Add Section

        #region Remove Section
        [HttpDelete("delete/{note}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Remove(Note note)
        {
            try
            {
                myNoteService.Remove(note);
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
        [HttpDelete("deleteById/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveById(Guid id)
        {
            try
            {
                myNoteService.RemoveById(id);
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
        #endregion Remove Section

        #region Modify Section
        [HttpPut("update")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Update([FromBody] Note note)
        {
            Note? result = myNoteService.Update(note);
            
            return result is not null ? Ok(result) : BadRequest("Note cannot be modified.");
        }
        #endregion Modify Section
    }
}
