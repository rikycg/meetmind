using MeetMind.Domain.Meetings;
using MeetMind.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class ActionItemConfiguration : IEntityTypeConfiguration<ActionItem>
{
    public void Configure(EntityTypeBuilder<ActionItem> builder) {
        builder.HasKey(a => a.Id);

        builder.HasOne<MeetingSummary>()
            .WithMany()
            .HasForeignKey(a => a.SummaryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.AssignedTo)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(a => a.Title)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(a => a.Description)
            .IsRequired(false);
        
        builder.Property(a => a.DueDate)
            .IsRequired(false);

        builder.Property(a => a.Status)
            .HasConversion<int>();

    }
}