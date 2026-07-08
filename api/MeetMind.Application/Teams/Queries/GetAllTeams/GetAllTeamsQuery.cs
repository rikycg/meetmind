using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetAllTeams;

public record GetAllTeamsQuery() : IRequest<IEnumerable<TeamResponse>>;
