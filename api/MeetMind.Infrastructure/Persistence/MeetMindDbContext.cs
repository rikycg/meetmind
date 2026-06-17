using Microsoft.EntityFrameworkCore;
using MeetMind.Domain.Users;
using MeetMind.Domain.Meetings;
using MeetMind.Domain.Teams;

namespace MeetMind.Infrastructure.Persistence;

public class MeetMindDbContext : DbContext
{
    public MeetMindDbContext(DbContextOptions<MeetMindDbContext> options) 
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Meeting> Meetings => Set<Meeting>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<AudioRecording> AudioRecordings => Set<AudioRecording>();
    public DbSet<Transcript> Transcripts => Set<Transcript>();
    public DbSet<TranscriptSegment> TranscriptSegments => Set<TranscriptSegment>();
    public DbSet<MeetingSummary> MeetingSummaries => Set<MeetingSummary>();
    public DbSet<KeyDecision> KeyDecisions => Set<KeyDecision>();
    public DbSet<ActionItem> ActionItems => Set<ActionItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Todos los DateTime se guardan como timestamptz
    foreach (var entity in modelBuilder.Model.GetEntityTypes())
    {
        foreach (var property in entity.GetProperties()
            .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
        {
            property.SetColumnType("timestamp with time zone");
        }
    }

    modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetMindDbContext).Assembly);
}
}
