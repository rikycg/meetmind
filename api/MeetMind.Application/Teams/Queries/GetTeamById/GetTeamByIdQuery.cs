using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetTeamById;

public record GetTeamByIdQuery(Guid Id) : IRequest<TeamResponse?>;
