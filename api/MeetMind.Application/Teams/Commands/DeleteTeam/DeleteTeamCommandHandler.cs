using MediatR;
using MeetMind.Application.Interfaces;

namespace MeetMind.Application.Teams.Commands.DeleteTeam;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand>
{
    private readonly ITeamRepository _teamRepository;

    public DeleteTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken = default)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);

        if (team is null)
            throw new KeyNotFoundException($"Team with id '{request.Id}' was not found.");

        await _teamRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
