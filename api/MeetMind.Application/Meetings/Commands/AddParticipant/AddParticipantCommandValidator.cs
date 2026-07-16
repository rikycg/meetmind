using FluentValidation;

namespace MeetMind.Application.Meetings.Commands.AddParticipant;

public class AddParticipantCommandValidator : AbstractValidator<AddParticipantCommand>
{
    public AddParticipantCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is required");

        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("Meeting id is required");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required");
    }
}
