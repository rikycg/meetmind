using MeetMind.Domain.Common;

namespace MeetMind.Domain.Teams;
public sealed class Team : Entity {
    public string Name { get; private set; }
    public string Description { get; private set; }

    private Team() : base() { }
    
    private Team(string name, string description) {
        Name = name;
        Description = description;
    }

    public static Team Create(string name, string description) {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("name is empty.");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("description is empty.");

        string nameCleaned = name.Trim();
        string descriptionCleaned = description.Trim();

        return new Team(nameCleaned, descriptionCleaned);
    }

    public void UpdateInfo(string name, string description) {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("name is empty.");

        Name = name.Trim();
        Description = description?.Trim() ?? Description;
        UpdatedAt = DateTime.UtcNow;
    }
}
