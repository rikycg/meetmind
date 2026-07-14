using MediatR;
using MeetMind.Application.Teams.Commands.AddTeamMember;
using MeetMind.Application.Teams.Commands.ChangeTeamMemberRole;
using MeetMind.Application.Teams.Commands.RemoveTeamMember;
using MeetMind.Application.Teams.Queries.GetTeamMembersByTeamId;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/teams/{teamId}/members")]
public class TeamMembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamMembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add(Guid teamId, AddTeamMemberCommand command, CancellationToken cancellationToken = default)
    {
        if (teamId != command.TeamId)
        {
            return BadRequest("The team id in the URL does not match the request");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid teamId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetTeamMembersByTeamIdQuery(teamId), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id}/role")]
    public async Task<IActionResult> ChangeRole(Guid teamId, Guid id, ChangeTeamMemberRoleCommand command, CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
        {
            return BadRequest("The member id in the URL does not match the request");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(Guid teamId, Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new RemoveTeamMemberCommand(id), cancellationToken);
        return NoContent();
    }
}
