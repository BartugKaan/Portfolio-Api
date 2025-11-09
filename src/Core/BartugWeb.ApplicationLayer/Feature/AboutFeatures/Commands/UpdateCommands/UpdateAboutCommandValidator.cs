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
        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl is required.")
            .MinimumLength(3).WithMessage("ImageUrl cannot exceed 3 characters.");
        RuleFor(x => x.Stacks)
            .NotEmpty().WithMessage("Stacks are required.");
        RuleFor(x => x.Educations)
            .NotEmpty().WithMessage("Educations are required.");
        RuleFor(x => x.Experience)
            .NotEmpty().WithMessage("Experience are required.");    
    }
}