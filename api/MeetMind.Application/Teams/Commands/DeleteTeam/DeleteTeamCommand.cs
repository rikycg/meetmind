using MediatR;

namespace MeetMind.Application.Teams.Commands.DeleteTeam;

public record DeleteTeamCommand(Guid Id) : IRequest;
