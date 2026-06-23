using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class AudioRecordingConfiguration : IEntityTypeConfiguration<AudioRecording>
{
    public void Configure(EntityTypeBuilder<AudioRecording> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne<Meeting>()
            .WithMany()
            .HasForeignKey(a => a.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.FileUrl)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(a => a.Duration)
            .IsRequired();

        builder.Property(a => a.FileSize)
            .IsRequired();

        builder.Property(a => a.Format)
            .IsRequired()
            .HasMaxLength(50);

    }
}