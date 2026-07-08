using MediatR;

namespace MeetMind.Application.Teams.Commands.RemoveTeamMember;

public record RemoveTeamMemberCommand(Guid Id) : IRequest;
