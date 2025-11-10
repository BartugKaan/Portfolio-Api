using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.RemoveCommands;

public record RemoveGetInTouchCommand(string Id) : IRequest<string>;
