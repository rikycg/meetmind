using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class TranscriptConfiguration : IEntityTypeConfiguration<Transcript>
{
    public void Configure(EntityTypeBuilder<Transcript> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasOne<Meeting>()
            .WithMany()
            .HasForeignKey(t => t.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.MeetingId)
            .IsUnique();

        builder.Property(t => t.Language)
            .HasConversion<int>();

        builder.Property(t => t.Content)
            .IsRequired();
    }
}
