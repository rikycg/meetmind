using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class MeetingSummaryConfiguration : IEntityTypeConfiguration<MeetingSummary>
{
    public void Configure(EntityTypeBuilder<MeetingSummary> builder)
    {
        builder.HasKey(ms => ms.Id);

        builder.HasOne<Meeting>()
            .WithMany()
            .HasForeignKey(ms => ms.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ms => ms.MeetingId)
            .IsUnique();

        builder.Property(ms => ms.Summary)
            .IsRequired();

        builder.Ignore(ms => ms.KeyDecisions);
        builder.Ignore(ms => ms.ActionItems);
    }
}
