using MeetMind.Application.Interfaces;
using MeetMind.Domain.Teams;
using Microsoft.EntityFrameworkCore;

namespace MeetMind.Infrastructure.Persistence.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly MeetMindDbContext _context;

    public TeamRepository(MeetMindDbContext context)
    {
        _context = context;
    }

    public async Task<Team?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Teams.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Team?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Teams
            .FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Team>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Teams.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
    {
        await _context.Teams.AddAsync(team, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Team team, CancellationToken cancellationToken = default)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Teams
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Teams
            .AnyAsync(t => t.Name == name, cancellationToken);
    }
}
