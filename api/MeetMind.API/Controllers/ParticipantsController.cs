using MediatR;
using MeetMind.Application.Meetings.Commands.AddParticipant;
using MeetMind.Application.Meetings.Commands.LeaveParticipant;
using MeetMind.Application.Meetings.Commands.RemoveParticipant;
using MeetMind.Application.Meetings.Queries.GetParticipantsByMeetingId;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/meetings/{meetingId}/participants")]
public class ParticipantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ParticipantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add(Guid meetingId, AddParticipantCommand command, CancellationToken cancellationToken = default)
    {
        if (meetingId != command.MeetingId)
        {
            return BadRequest("The meeting id in the URL does not match the request");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid meetingId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetParticipantsByMeetingIdQuery(meetingId), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id}/leave")]
    public async Task<IActionResult> Leave(Guid meetingId, Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new LeaveParticipantCommand(id), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid meetingId, Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new RemoveParticipantCommand(id), cancellationToken);
        return NoContent();
    }
}
