using FluentValidation;

namespace MeetMind.Application.Meetings.Commands.CreateActionItem;

public class CreateActionItemCommandValidator : AbstractValidator<CreateActionItemCommand>
{
    public CreateActionItemCommandValidator()
    {
        RuleFor(x => x.SummaryId)
            .NotEmpty().WithMessage("Summary id is required");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters")
            .When(x => x.Description is not null);

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future")
            .When(x => x.DueDate.HasValue);
    }
}
