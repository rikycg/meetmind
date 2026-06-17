using MeetMind.Domain.Meetings;
using MeetMind.Domain.Teams;
using MeetMind.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(m => m.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(m => m.HostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Team>()
            .WithMany()
            .HasForeignKey(m => m.TeamId)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.Property(m => m.ScheduledAt)
            .IsRequired();

        builder.Property(m => m.StartedAt)
            .IsRequired(false);

        builder.Property(m => m.EndedAt)
            .IsRequired(false);

        builder.Property(m => m.Status)
            .HasConversion<int>();
    }
}