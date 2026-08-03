using MediatR;
using MeetMind.Application.Meetings.Commands.CancelMeeting;
using MeetMind.Application.Meetings.Commands.CompleteMeeting;
using MeetMind.Application.Meetings.Commands.CreateMeeting;
using MeetMind.Application.Meetings.Commands.DeleteMeeting;
using MeetMind.Application.Meetings.Commands.StartMeeting;
using MeetMind.Application.Meetings.Commands.UpdateMeeting;
using MeetMind.Application.Meetings.Common;
using MeetMind.Application.Meetings.Queries.GetAllMeetings;
using MeetMind.Application.Meetings.Queries.GetMeetingById;
using MeetMind.Application.Meetings.Queries.GetMeetingsByHostId;
using MeetMind.Application.Meetings.Queries.GetMeetingsByStatus;
using MeetMind.Application.Meetings.Queries.GetMeetingsByTeamId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MeetingsController : ControllerBase {

    private readonly IMediator _mediator;

    public MeetingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMeetingCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] Guid? teamId, [FromQuery] Guid? hostId, CancellationToken cancellationToken = default)
    {
        IEnumerable<MeetingResponse> meetings;
        if (status is not null)
        {
            meetings = await _mediator.Send(new GetMeetingsByStatusQuery(status), cancellationToken);
        } else if (teamId is not null)
        {
            meetings = await _mediator.Send(new GetMeetingsByTeamIdQuery(teamId.Value), cancellationToken);
        } else if (hostId is not null)
        {
            meetings = await _mediator.Send(new GetMeetingsByHostIdQuery(hostId.Value), cancellationToken);
        } else
        {
            meetings = await _mediator.Send(new GetAllMeetingsQuery(), cancellationToken);
        }

        return Ok(meetings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetMeetingByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateMeetingCommand command, CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
        {
            return BadRequest("The meeting id to update is not the same of request");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/start")]
    public async Task<IActionResult> Start(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new StartMeetingCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new CompleteMeetingCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new CancelMeetingCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteMeetingCommand(id), cancellationToken);
        return NoContent();
    }
}
