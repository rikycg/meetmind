using FluentValidation;

namespace MeetMind.Application.Meetings.Commands.CreateMeetingSummary;

public class CreateMeetingSummaryCommandValidator : AbstractValidator<CreateMeetingSummaryCommand>
{
    public CreateMeetingSummaryCommandValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("Meeting id is required");

        RuleFor(x => x.Summary)
            .NotEmpty().WithMessage("Summary is required");
    }
}
