using MediatR;
using MeetMind.Application.Interfaces;

namespace MeetMind.Application.Teams.Commands.RemoveTeamMember;

public class RemoveTeamMemberCommandHandler : IRequestHandler<RemoveTeamMemberCommand>
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public RemoveTeamMemberCommandHandler(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task Handle(RemoveTeamMemberCommand request, CancellationToken cancellationToken = default)
    {
        var member = await _teamMemberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (member is null)
            throw new KeyNotFoundException($"Team member with id '{request.Id}' was not found.");

        await _teamMemberRepository.RemoveFromTeamAsync(request.Id, cancellationToken);
    }
}
