using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.UpdateCommands;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.ProjectImgUrl)
            .NotEmpty().WithMessage("Project image url is required.");
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
