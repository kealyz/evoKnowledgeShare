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
                IEnumerable<Note> notes =myNoteService.GetAll();
                if (notes != null && notes.Any())
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

        [HttpGet("{noteId}")]
        public IActionResult GetById(Guid noteId)
        {
            try
            {
                Note? note=myNoteService.GetById(noteId);
                return Ok(note);
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
        [HttpGet("byUserId/{userId}")]
        public IActionResult GetByUserId(Guid userId)
        {
            try
            {
                IEnumerable<Note> notes = myNoteService.GetByUserId(userId);
                if (notes != null && notes.Any())
                {
                    return Ok(notes);
                }
                else return NotFound();
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
                IEnumerable<Note> notes = myNoteService.GetByTopicId(topicId);
                if (notes != null && notes.Any())
                {
                    return Ok(notes);
                }
                else return NotFound();
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
                Note? note = myNoteService.GetByDescription(description);
                if (note != null)
                {
                    return Ok(note);
                }
                else return NotFound();
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
                Note? note = myNoteService.GetByTitle(title);
                if (note != null)
                {
                    return Ok(note);
                }
                else return NotFound();
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
        [HttpDelete("delete")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Remove([FromBody] Note note)
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
                return Ok();
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
            try
            {
                Note? result = myNoteService.Update(note);
                return result is not null ? Ok(result) : BadRequest("Note cannot be modified.");
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
        #endregion Modify Section
    }
}
