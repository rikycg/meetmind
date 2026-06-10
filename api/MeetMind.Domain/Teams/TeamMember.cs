using MeetMind.Domain.Common;

namespace MeetMind.Domain.Teams;

public sealed class TeamMember : Entity {
    public Guid UserId { get; private set; }
    public Guid TeamId { get; private set; }
    public TeamMemberRole Role { get; private set; }
    public DateTime JoinedAt { get; private set; }

    private TeamMember(): base () {} 

    private TeamMember(Guid userId, Guid teamId, TeamMemberRole role) {
        UserId = userId;
        TeamId = teamId;
        Role = role;
        JoinedAt = DateTime.UtcNow;
    }

    public static TeamMember Create(Guid userId, Guid teamId, TeamMemberRole role) {
        if (userId == Guid.Empty)
            throw new ArgumentException("userId is empty.");

        if (teamId == Guid.Empty)
            throw new ArgumentException("teamId is empty.");

        return new TeamMember(userId, teamId, role);
    }

    public void ChangeRole(TeamMemberRole role) {
        Role = role;
        UpdatedAt = DateTime.UtcNow;
    }
}