using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.CreateCommands;

public class CreateEducationCommandValidator : AbstractValidator<CreateEducationCommand>
{
    public CreateEducationCommandValidator()
    {
        RuleFor(x => x.AboutId)
            .NotEmpty().WithMessage("AboutId is required.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage("SchoolName is required.")
            .MaximumLength(300).WithMessage("SchoolName cannot exceed 300 characters.");

        RuleFor(x => x.StartYear)
            .NotEmpty().WithMessage("StartYear is required.")
            .GreaterThan(1900).WithMessage("StartYear must be greater than 1900.")
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage("StartYear cannot be in the future.");

        RuleFor(x => x.EndYear)
            .GreaterThan(x => x.StartYear)
            .When(x => x.EndYear.HasValue)
            .WithMessage("EndYear must be after StartYear.")
            .LessThanOrEqualTo(DateTime.UtcNow.Year + 10)
            .When(x => x.EndYear.HasValue)
            .WithMessage("EndYear cannot be more than 10 years in the future.");
    }
}
