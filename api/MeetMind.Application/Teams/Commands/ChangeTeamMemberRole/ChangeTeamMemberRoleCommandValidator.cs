using FluentValidation;

namespace MeetMind.Application.Teams.Commands.ChangeTeamMemberRole;

public class ChangeTeamMemberRoleCommandValidator : AbstractValidator<ChangeTeamMemberRoleCommand>
{
    public ChangeTeamMemberRoleCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Member id is required");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required");
    }
}
