using MediatR;
using MeetMind.Application.Users.Commands.CreateUser;
using MeetMind.Application.Users.Commands.DeleteUser;
using MeetMind.Application.Users.Commands.UpdateUserName;
using MeetMind.Application.Users.Queries.GetAllUsers;
using MeetMind.Application.Users.Queries.GetUserByEmail;
using MeetMind.Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? email, CancellationToken cancellationToken = default)
    {
        if (email is not null)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(email), cancellationToken);
            return user is null ? NotFound() : Ok(user);
        }

        var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserNameCommand command, CancellationToken cancellationToken = default)
    {
        if (id != command.Id) {
            return BadRequest("The user id to update is not the same of request");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteUserCommand(id), cancellationToken);
        return NoContent();
    }
}