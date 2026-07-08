using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetTeamByName;

public record GetTeamByNameQuery(string Name) : IRequest<TeamResponse?>;
