using MediatR;
using MeetMind.Application.Meetings.Commands.CreateKeyDecision;
using MeetMind.Application.Meetings.Commands.DeleteKeyDecision;
using MeetMind.Application.Meetings.Queries.GetKeyDecisionsBySummaryId;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/summaries/{summaryId}/key-decisions")]
public class KeyDecisionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public KeyDecisionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid summaryId, CreateKeyDecisionCommand command, CancellationToken cancellationToken = default)
    {
        if (summaryId != command.SummaryId)
        {
            return BadRequest("The summary id in the URL does not match the request");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid summaryId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetKeyDecisionsBySummaryIdQuery(summaryId), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid summaryId, Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteKeyDecisionCommand(id), cancellationToken);
        return NoContent();
    }
}
