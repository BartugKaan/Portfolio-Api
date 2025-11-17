using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.UpdateCommand;

public class UpdateBlogPostValidator : AbstractValidator<UpdateBlogPostCommand>
{
    public UpdateBlogPostValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id cannot be empty");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");
        RuleFor(x => x.BlogContent)
            .NotEmpty().WithMessage("Blog content cannot be empty")
            .MinimumLength(100).WithMessage("Blog content must be at least 100 characters long");
        RuleFor(x => x.Keywords)
            .NotEmpty().WithMessage("Keywords cannot be empty");    
    }
}