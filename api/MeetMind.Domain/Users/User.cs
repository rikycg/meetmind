using MeetMind.Domain.Common;

namespace MeetMind.Domain.Users;

public sealed class User : Entity
{
    public Email Email { get; private set; }
    public FullName FullName { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }

    private User() : base() { }  // Para EF Core

    private User(Email email, FullName fullName, string passwordHash, UserRole role)
        : base()
    {
        Email = email;
        FullName = fullName;
        PasswordHash = passwordHash;
        Role = role;
    }

    public static User Create(string email, string firstName, string lastName, string passwordHash)
    {
        return new User(
            Email.Create(email),
            FullName.Create(firstName, lastName),
            passwordHash,
            UserRole.User
        );
    }

    public void UpdateName(string firstName, string lastName)
    {
        FullName = FullName.Create(firstName, lastName);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeRole(UserRole newRole)
    {
        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
    }
}
