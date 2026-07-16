using FluentValidation;

namespace MeetMind.Application.Meetings.Commands.CreateTranscript;

public class CreateTranscriptCommandValidator : AbstractValidator<CreateTranscriptCommand>
{
    public CreateTranscriptCommandValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("Meeting id is required");

        RuleFor(x => x.Language)
            .NotEmpty().WithMessage("Language is required")
            .MaximumLength(10).WithMessage("Language cannot exceed 10 characters");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");
    }
}
