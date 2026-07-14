using MediatR;
using MeetMind.Application.Meetings.Commands.CreateMeetingSummary;
using MeetMind.Application.Meetings.Commands.DeleteMeetingSummary;
using MeetMind.Application.Meetings.Queries.GetMeetingSummaryByMeetingId;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/meetings/{meetingId}/summary")]
public class MeetingSummariesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingSummariesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid meetingId, CreateMeetingSummaryCommand command, CancellationToken cancellationToken = default)
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
        var result = await _mediator.Send(new GetMeetingSummaryByMeetingIdQuery(meetingId), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid meetingId, Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteMeetingSummaryCommand(id), cancellationToken);
        return NoContent();
    }
}
