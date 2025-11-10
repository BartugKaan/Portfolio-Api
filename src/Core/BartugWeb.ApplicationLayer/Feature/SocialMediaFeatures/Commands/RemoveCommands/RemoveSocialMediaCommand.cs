using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.RemoveCommands;

public record RemoveSocialMediaCommand(string Id) : IRequest<string>;
