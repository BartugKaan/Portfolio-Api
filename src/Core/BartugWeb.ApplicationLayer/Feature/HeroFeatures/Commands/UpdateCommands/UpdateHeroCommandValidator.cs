using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.UpdateCommands;

public class UpdateHeroCommandValidator : AbstractValidator<UpdateHeroCommand>
{
    public UpdateHeroCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.HeroImageUrl)
            .NotEmpty().WithMessage("Hero image url is required.");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(150).WithMessage("Title cannot exceed 150 characters.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name cannot exceed 150 characters.");
        RuleFor(x => x.JobTitles)
            .NotEmpty().WithMessage("Job titles are required.")
            .MaximumLength(250).WithMessage("Job titles cannot exceed 250 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}
