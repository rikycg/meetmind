using FluentValidation;

namespace MeetMind.Application.Meetings.Commands.CreateAudioRecording;

public class CreateAudioRecordingCommandValidator : AbstractValidator<CreateAudioRecordingCommand>
{
    public CreateAudioRecordingCommandValidator()
    {
        RuleFor(x => x.MeetingId)
            .NotEmpty().WithMessage("Meeting id is required");

        RuleFor(x => x.FileUrl)
            .NotEmpty().WithMessage("File URL is required");

        RuleFor(x => x.Duration)
            .GreaterThan(0).WithMessage("Duration must be greater than zero");

        RuleFor(x => x.FileSize)
            .GreaterThan(0).WithMessage("File size must be greater than zero");

        RuleFor(x => x.Format)
            .NotEmpty().WithMessage("Format is required");
    }
}
