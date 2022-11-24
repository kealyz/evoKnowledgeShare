using evoKnowledgeShare.Backend.Models;
using evoKnowledgeShare.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace evoKnowledgeShare.Backend.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class HistoryController : ControllerBase
    {
        private HistoryService myHistoryService;

        public HistoryController(HistoryService myHistoryService)
        {
            this.myHistoryService = myHistoryService;
        }

        [HttpGet("Histories")]
        public IActionResult GetHistories()
        {
            IEnumerable<History> result = myHistoryService.GetAll();
            return result.Any() ? Ok(result) : Problem(statusCode: StatusCodes.Status404NotFound, title: "No history found.");
        }

        [HttpGet("History/{id}")]
        public IActionResult GetHistoryById(Guid id)
        {
            return Ok(myHistoryService.GetById(id));
        }

        [HttpPost("Create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateHistoryAsync([FromBody] History history)
        {
            History? result = await myHistoryService.CreateHistory(history);
            return result is not null ? Created(nameof(CreateHistoryAsync), result) : BadRequest("History cannot be added");
        }
    }
}
