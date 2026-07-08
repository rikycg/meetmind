using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Commands.UpdateTeam;

public record UpdateTeamCommand(Guid Id, string Name, string Description) : IRequest<TeamResponse>;
