using MeetMind.Domain.Teams;

namespace MeetMind.Application.Interfaces;

public interface ITeamMemberRepository {
    Task<IEnumerable<TeamMember>> GetAllByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TeamMember>> GetAllByRoleAsync(TeamMemberRole role, CancellationToken cancellationToken = default);
    Task<TeamMember?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<TeamMember?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddToTeamAsync(TeamMember teamMember, CancellationToken cancellationToken = default);
    Task RemoveFromTeamAsync(Guid id, CancellationToken cancellationToken = default);
}