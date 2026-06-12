using MeetMind.Domain.Common;

namespace MeetMind.Domain.Meetings;

public sealed class ActionItem : Entity {
    public Guid SummaryId { get; private set; }
    public Guid? AssignedTo { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public ActionItemStatus Status { get; private set; }

    private ActionItem() : base() {}

    private ActionItem(Guid summaryId, string title, string? description, Guid? assignedTo, DateTime? dueDate) {
        SummaryId = summaryId;
        Title = title;
        Status = ActionItemStatus.Pending;
        Description = description;
        AssignedTo = assignedTo;
        DueDate = dueDate;
    }

    public static ActionItem Create(Guid summaryId, string title, string? description, Guid? assignedTo, DateTime? dueDate) {
        if (summaryId == Guid.Empty) 
            throw new ArgumentException("summaryId is empty.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("title is empty.");
        
        return new ActionItem(summaryId, title, description, assignedTo, dueDate);
    }

    public void Start() {
        if (Status != ActionItemStatus.Pending)
            throw new InvalidOperationException("Only pending items can be started.");

        Status = ActionItemStatus.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete() {
        if (Status != ActionItemStatus.InProgress)
            throw new InvalidOperationException("Only in progress items can be completed.");

        Status = ActionItemStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel() {
        if (Status == ActionItemStatus.Completed)
            throw new InvalidOperationException("Completed items cannot be cancelled.");

        Status = ActionItemStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid userId) {
        AssignedTo = userId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDueDate(DateTime dueDate) {
        DueDate = dueDate;
        UpdatedAt = DateTime.UtcNow;
    }
}