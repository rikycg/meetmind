using MediatR;
using MeetMind.Application.Meetings.Commands.CreateTranscript;
using MeetMind.Application.Meetings.Commands.DeleteTranscript;
using MeetMind.Application.Meetings.Queries.GetTranscriptByMeetingId;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/meetings/{meetingId}/transcript")]
public class TranscriptsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TranscriptsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid meetingId, CreateTranscriptCommand command, CancellationToken cancellationToken = default)
    {
        if (meetingId != command.MeetingId)
        {
            return BadRequest("The meeting id in the URL does not match the request");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid meetingId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetTranscriptByMeetingIdQuery(meetingId), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid meetingId, Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteTranscriptCommand(id), cancellationToken);
        return NoContent();
    }
}
