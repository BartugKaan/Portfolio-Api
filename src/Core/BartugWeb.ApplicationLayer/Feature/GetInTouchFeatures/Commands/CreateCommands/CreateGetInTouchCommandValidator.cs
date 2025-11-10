using FluentValidation;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.CreateCommands;

public class CreateGetInTouchCommandValidator : AbstractValidator<CreateGetInTouchCommand>
{
    public CreateGetInTouchCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
        RuleFor(x => x.ContactName)
            .NotEmpty().WithMessage("Contact name is required.")
            .MaximumLength(200).WithMessage("Contact name cannot exceed 200 characters.");
        RuleFor(x => x.ContactEmail)
            .NotEmpty().WithMessage("Contact email is required.")
            .EmailAddress().WithMessage("Contact email must be valid.");
        RuleFor(x => x.ContactMessage)
            .NotEmpty().WithMessage("Contact message is required.")
            .MaximumLength(2000).WithMessage("Contact message cannot exceed 2000 characters.");
    }
}
