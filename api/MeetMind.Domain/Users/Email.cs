using System.Text.RegularExpressions;
using MeetMind.Domain.Common;

namespace MeetMind.Domain.Users;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        email = email.Trim().ToLowerInvariant();

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException($"'{email}' is not a valid email.");

        return new Email(email);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}