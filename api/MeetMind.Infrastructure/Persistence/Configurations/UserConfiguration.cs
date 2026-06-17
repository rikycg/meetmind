using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MeetMind.Domain.Users;

namespace MeetMind.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(256);

            email.HasIndex(e => e.Value).IsUnique();
        });

        builder.OwnsOne(u => u.FullName, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .IsRequired()
                .HasMaxLength(100);

            name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Role)
            .HasConversion<int>();
    }
}