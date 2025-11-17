using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;

public class CreateAboutCommandValidator : AbstractValidator<CreateAboutCommand>
{
    public CreateAboutCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");
        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image is required.");
    }
}