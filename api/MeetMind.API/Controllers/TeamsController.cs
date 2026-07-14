using MediatR;
using MeetMind.Application.Teams.Commands.CreateTeam;
using MeetMind.Application.Teams.Commands.DeleteTeam;
using MeetMind.Application.Teams.Commands.UpdateTeam;
using MeetMind.Application.Teams.Queries.GetAllTeams;
using MeetMind.Application.Teams.Queries.GetTeamById;
using MeetMind.Application.Teams.Queries.GetTeamByName;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTeamCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? name, CancellationToken cancellationToken = default)
    {
        if (name is not null)
        {
            var team = await _mediator.Send(new GetTeamByNameQuery(name), cancellationToken);
            return team is null ? NotFound() : Ok(team);
        }

        var result = await _mediator.Send(new GetAllTeamsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetTeamByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateTeamCommand command, CancellationToken cancellationToken = default)
    {
        if (id != command.Id)
        {
            return BadRequest("The team id to update does not match the request");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteTeamCommand(id), cancellationToken);
        return NoContent();
    }
}
