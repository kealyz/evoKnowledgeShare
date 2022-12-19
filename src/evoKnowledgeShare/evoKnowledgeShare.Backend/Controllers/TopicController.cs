using evoKnowledgeShare.Backend.DTO;
using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace evoKnowledgeShare.Backend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase {
        private TopicService myTopicService;

        public TopicController(TopicService myTopicService) {
            this.myTopicService = myTopicService;
        }

        [HttpGet("Topics")]
        public IActionResult GetTopics() {
            IEnumerable<Topic> result = myTopicService.GetAll();
            return result.Any() ? Ok(result) : Problem(statusCode: StatusCodes.Status404NotFound, title: "No topics found.");
        }

        [HttpGet("TopicID/{id}")]
        public IActionResult GetTopicsById(Guid id) {
            try {
                return Ok(myTopicService.GetById(id));
            } catch (ArgumentNullException) {
                throw;
            } catch (InvalidOperationException) {
                throw;
            }
        }

        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTopicAsync([FromBody] TopicDTO topicDTO) {
            Topic? result = await myTopicService.AddAsync(topicDTO);
            return result is not null ? Created(nameof(CreateTopicAsync), result) : BadRequest("Topic cannot be added.");
        }

        [HttpDelete("Delete")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTopic(Guid id) {
            try {
                myTopicService.RemoveById(id);
                return NoContent();
            } catch (KeyNotFoundException) {
                return NotFound();
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}
