using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.CreateCommands;

public class CreateStackCommandValidator : AbstractValidator<CreateStackCommand>
{
    private static readonly string[] ValidCategories = {
        "Backend",
        "Frontend",
        "Database",
        "AI Development",
        "Version Control"
    };

    public CreateStackCommandValidator()
    {
        RuleFor(x => x.AboutId)
            .NotEmpty().WithMessage("AboutId is required.");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.")
            .MaximumLength(100).WithMessage("Category cannot exceed 100 characters.")
            .Must(category => ValidCategories.Contains(category))
            .WithMessage($"Category must be one of: {string.Join(", ", ValidCategories)}");

        RuleFor(x => x.Technology)
            .NotEmpty().WithMessage("Technology is required.")
            .MaximumLength(200).WithMessage("Technology cannot exceed 200 characters.");

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0).WithMessage("Order must be greater than or equal to 0.");
    }
}
