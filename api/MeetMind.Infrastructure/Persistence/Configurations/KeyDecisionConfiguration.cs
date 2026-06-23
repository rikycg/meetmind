using MeetMind.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class KeyDecisionConfiguration : IEntityTypeConfiguration<KeyDecision>
{
    public void Configure(EntityTypeBuilder<KeyDecision> builder)
    {
        builder.HasKey(k => k.Id);

        builder.HasOne<MeetingSummary>()
            .WithMany()
            .HasForeignKey(k => k.SummaryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(k => k.Content)
            .IsRequired();
    }
}
