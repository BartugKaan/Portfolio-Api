using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;

public class UpdateAboutCommandValidator : AbstractValidator<UpdateAboutCommand>
{
    public UpdateAboutCommandValidator()
    {
        RuleFor(x => x.AboutId)
            .NotEmpty().WithMessage("AboutId is required.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");
    }
}