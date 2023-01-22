using evoKnowledgeShare.Backend.DTO;
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
        [HttpGet("")]
        public IActionResult GetNotes()
        {
            try
            {
                IEnumerable<Note> notes =myNoteService.GetAll();
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

        [HttpGet("{noteId}")]
        public IActionResult GetById(Guid noteId)
        {
            try
            {
                Note note=myNoteService.GetById(noteId);
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
        public IActionResult GetRangeByUserId(Guid userId)
        {
            try
            {
                IEnumerable<Note> notes = myNoteService.GetRangeByUserId(userId);
                if (notes.Any())
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
        public IActionResult GetRangeByTopicId(Guid topicId)
        {
            try
            {
                IEnumerable<Note> notes = myNoteService.GetRangeBytTopicId(topicId);
                if (notes.Any())
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
                return Ok(myNoteService.GetByDescription(description));
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
        [HttpGet("byTitle/{title}")]
        public IActionResult GetByTitle(string title)
        {
            try
            {
                return Ok(myNoteService.GetByTitle(title));
            }
            catch (KeyNotFoundException)
            {
                return NotFound() ;
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion Get Section

        #region Add Section
        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAsync([FromBody] NoteMdDTO noteMdDTO)
        {
            try
            {
                Note result = await myNoteService.AddAsync(noteMdDTO.note, noteMdDTO.mdRaw);
                return Created(nameof(AddAsync), result);
            }
            catch (ArgumentException)
            {
                return BadRequest("Note cannot be added.");
            }catch(Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost("createRange")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRangeAsync([FromBody] IEnumerable<Note> notes)
        {
            try
            {
                IEnumerable<Note> result = await myNoteService.AddRangeAsync(notes);
                return Created(nameof(AddRangeAsync), result);
            }
            catch (ArgumentException)
            {
                return BadRequest("Note cannot be added.");
            }
        }

        #endregion Add Section

        #region Remove Section
        [HttpDelete("")]
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
        [HttpDelete("byId/{id}")]
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
        [HttpPut("")]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult Update([FromBody] NoteMdDTO noteMdDto)
        {
            try
            {
                Note result = myNoteService.Update(noteMdDto.note, noteMdDto.mdRaw, noteMdDto.incrementSize);
                return Ok(result);
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
