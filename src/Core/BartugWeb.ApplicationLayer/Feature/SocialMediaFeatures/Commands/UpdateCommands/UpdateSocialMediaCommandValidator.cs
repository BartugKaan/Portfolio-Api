using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;

public class UpdateSocialMediaCommandValidator : AbstractValidator<UpdateSocialMediaCommand>
{
    public UpdateSocialMediaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.LinkUrl)
            .NotEmpty().WithMessage("Link url is required.");
    }
}
