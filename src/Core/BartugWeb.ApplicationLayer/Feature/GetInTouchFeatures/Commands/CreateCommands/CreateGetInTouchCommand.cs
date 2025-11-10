using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.CreateCommands;

public record CreateGetInTouchCommand(
    string Title,
    string Description,
    string ContactName,
    string ContactEmail,
    string ContactMessage) : IRequest<string>;
