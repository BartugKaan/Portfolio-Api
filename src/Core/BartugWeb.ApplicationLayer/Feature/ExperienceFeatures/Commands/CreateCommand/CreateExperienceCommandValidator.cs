using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.CreateCommand;

public class CreateExperienceCommandValidator : AbstractValidator<CreateExperienceCommand>
{
    public CreateExperienceCommandValidator()
    {
        RuleFor(x => x.AboutId)
            .NotEmpty().WithMessage("About Id cannot be empty");
        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Company cannot be empty")
            .MaximumLength(200).WithMessage("Company cannot exceed 200 characters");
        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position cannot be empty")
            .MaximumLength(200).WithMessage("Position cannot exceed 200 characters");
        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location cannot be empty")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters");
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start Date cannot be empty")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Start Date cannot be in the future");
        
        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("End Date cannot be in the future");
    }
}