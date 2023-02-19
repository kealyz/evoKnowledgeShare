using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Repositories;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace evoKnowledgeShare.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private TopicService myTopicService;

        private TreeViewService myTreeViewService;

        public TopicController(TopicService topicService, TreeViewService treeViewService)
        {
            myTopicService = topicService;
            myTreeViewService = treeViewService;
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTopics()
        {
            try
            {
                return Ok(myTopicService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTopicById(Guid id)
        {
            try
            {
                return Ok(myTopicService.GetById(id));
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
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

        [HttpGet("ByTitle/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTopicsByTitle(string title)
        {
            try
            {
                return Ok(myTopicService.GetByTitle(title));
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
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

        [HttpGet("TreeView")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTreeView()
        {
            return Ok(myTreeViewService.GetTreeView());
        }

        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateTopicAsync([FromBody] TopicDTO topicDTO)
        {
            try
            {
                return Created(nameof(CreateTopicAsync), await myTopicService.AddAsync(topicDTO));
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

        [HttpPost("Update")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult UpdateTopic([FromBody] Topic topic)
        {
            try
            {
                return Ok(myTopicService.Update(topic));
            }
            catch (ArgumentNullException)
            {
                return UnprocessableEntity();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTopic(Guid id)
        {
            try
            {
                myTopicService.RemoveById(id);
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
    }
}
