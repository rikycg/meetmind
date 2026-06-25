using MeetMind.Application.Interfaces;
using MeetMind.Domain.Teams;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly MeetMindDbContext _context;

    public TeamMemberRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamMember>> GetAllByTeamIdAsync(Guid teamId, CancellationToken cancellationToken = default)
    {
        return await _context.TeamMembers
            .Where(tm => tm.TeamId == teamId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TeamMember>> GetAllByRoleAsync(TeamMemberRole role, CancellationToken cancellationToken = default)
    {
        return await _context.TeamMembers
            .Where(tm => tm.Role == role)
            .ToListAsync(cancellationToken);
    }

    public async Task<TeamMember?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.TeamMembers
            .FirstOrDefaultAsync(tm => tm.UserId == userId, cancellationToken);
    }

    public async Task<TeamMember?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.TeamMembers.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task AddToTeamAsync(TeamMember teamMember, CancellationToken cancellationToken = default)
    {
        await _context.TeamMembers.AddAsync(teamMember, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveFromTeamAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.TeamMembers
            .Where(tm => tm.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
