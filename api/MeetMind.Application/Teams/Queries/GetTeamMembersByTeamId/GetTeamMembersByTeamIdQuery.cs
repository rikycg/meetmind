using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Queries.GetTeamMembersByTeamId;

public record GetTeamMembersByTeamIdQuery(Guid TeamId) : IRequest<IEnumerable<TeamMemberResponse>>;
