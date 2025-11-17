using BartugWeb.DomainLayer.Enums;
using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.UpdateCommands;

public class UpdateBlogItemCommandValidator : AbstractValidator<UpdateBlogItemCommand>
{
    public UpdateBlogItemCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        RuleFor(x => x.BlogCategory)
            .IsInEnum().WithMessage($"Blog category must be a valid value of {nameof(BlogCategory)}.");
    }
}
