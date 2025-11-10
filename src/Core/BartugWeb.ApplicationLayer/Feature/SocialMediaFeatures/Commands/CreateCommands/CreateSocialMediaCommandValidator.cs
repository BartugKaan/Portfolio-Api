using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;

public class CreateSocialMediaCommandValidator : AbstractValidator<CreateSocialMediaCommand>
{
    public CreateSocialMediaCommandValidator()
    {
        RuleFor(x => x.IconUrl)
            .NotEmpty().WithMessage("Icon url is required.");
        RuleFor(x => x.LinkUrl)
            .NotEmpty().WithMessage("Link url is required.");
    }
}
