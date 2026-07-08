using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Commands.CreateTeam;

public record CreateTeamCommand(string Name, string Description) : IRequest<TeamResponse>;
