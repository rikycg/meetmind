using FluentValidation;

namespace MeetMind.Application.Meetings.Commands.CreateKeyDecision;

public class CreateKeyDecisionCommandValidator : AbstractValidator<CreateKeyDecisionCommand>
{
    public CreateKeyDecisionCommandValidator()
    {
        RuleFor(x => x.SummaryId)
            .NotEmpty().WithMessage("Summary id is required");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");
    }
}
