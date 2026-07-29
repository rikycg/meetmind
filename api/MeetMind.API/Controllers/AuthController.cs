using MediatR;
using MeetMind.Application.Auth.Commands.Login;
using MeetMind.Application.Auth.Commands.Logout;
using MeetMind.Application.Auth.Commands.RefreshToken;
using MeetMind.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace MeetMind.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Created("", result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutCommand command, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
