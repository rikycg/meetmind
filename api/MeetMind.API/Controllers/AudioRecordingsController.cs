using MediatR;
using MeetMind.Application.Meetings.Commands.CreateAudioRecording;
using MeetMind.Application.Meetings.Commands.DeleteAudioRecording;
using MeetMind.Application.Meetings.Queries.GetAudioRecordingsByMeetingId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/meetings/{meetingId}/audio-recordings")]
[Authorize]
public class AudioRecordingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AudioRecordingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Guid meetingId, CreateAudioRecordingCommand command, CancellationToken cancellationToken = default)
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
        var result = await _mediator.Send(new GetAudioRecordingsByMeetingIdQuery(meetingId), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid meetingId, Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteAudioRecordingCommand(id), cancellationToken);
        return NoContent();
    }
}
