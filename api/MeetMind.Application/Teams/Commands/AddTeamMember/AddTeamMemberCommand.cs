using MediatR;
using MeetMind.Application.Teams.Common;

namespace MeetMind.Application.Teams.Commands.AddTeamMember;

public record AddTeamMemberCommand(Guid UserId, Guid TeamId, string Role) : IRequest<TeamMemberResponse>;
