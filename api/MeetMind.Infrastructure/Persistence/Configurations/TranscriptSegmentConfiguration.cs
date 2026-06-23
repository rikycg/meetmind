using MeetMind.Domain.Meetings;
using MeetMind.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class TranscriptSegmentConfiguration : IEntityTypeConfiguration<TranscriptSegment>
{
    public void Configure(EntityTypeBuilder<TranscriptSegment> builder)
    {
        builder.HasKey(ts => ts.Id);

        builder.HasOne<Transcript>()
            .WithMany()
            .HasForeignKey(ts => ts.TranscriptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(ts => ts.SpeakerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(ts => ts.Text)
            .IsRequired();

        builder.Property(ts => ts.StartTime)
            .IsRequired();

        builder.Property(ts => ts.EndTime)
            .IsRequired();

        builder.Property(ts => ts.Confidence)
            .IsRequired();
    }
}
