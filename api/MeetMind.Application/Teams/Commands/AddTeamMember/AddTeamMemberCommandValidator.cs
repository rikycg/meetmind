using FluentValidation;

namespace MeetMind.Application.Teams.Commands.AddTeamMember;

public class AddTeamMemberCommandValidator : AbstractValidator<AddTeamMemberCommand>
{
    public AddTeamMemberCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id is required");

        RuleFor(x => x.TeamId)
            .NotEmpty().WithMessage("Team id is required");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required");
    }
}
