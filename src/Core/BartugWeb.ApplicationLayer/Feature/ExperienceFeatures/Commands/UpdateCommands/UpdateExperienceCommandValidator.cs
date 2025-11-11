using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.UpdateCommands;

public class UpdateExperienceCommandValidator : AbstractValidator<UpdateExperienceCommand>
{
    public UpdateExperienceCommandValidator()
    {
        RuleFor(x => x.ExperienceId)
            .NotEmpty().WithMessage("ExperienceId is required.");

        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Company is required.")
            .MaximumLength(200).WithMessage("Company cannot exceed 200 characters.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required.")
            .MaximumLength(200).WithMessage("Position cannot exceed 200 characters.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("StartDate cannot be in the future.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("EndDate must be after StartDate.");
    }
}
