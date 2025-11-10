using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.UpdateCommands;

public record UpdateGetInTouchCommand(
    string Id,
    string Title,
    string Description,
    string ContactName,
    string ContactEmail,
    string ContactMessage) : IRequest<string>;
