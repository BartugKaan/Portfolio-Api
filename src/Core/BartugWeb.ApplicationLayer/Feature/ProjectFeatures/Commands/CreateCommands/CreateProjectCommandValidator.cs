using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.CreateCommands;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.ProjectImage)
            .NotEmpty().WithMessage("Project image is required.");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        RuleFor(x => x.Keyword)
            .NotNull().WithMessage("Keywords are required.")
            .Must(list => list.Any()).WithMessage("At least one keyword is required.");
        RuleFor(x => x.ProjectUrl)
            .NotEmpty().WithMessage("Project url is required.");
    }
}
