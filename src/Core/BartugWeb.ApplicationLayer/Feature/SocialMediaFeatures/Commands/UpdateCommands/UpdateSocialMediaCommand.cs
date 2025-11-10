using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;

public record UpdateSocialMediaCommand(
    string Id,
    string IconUrl,
    string LinkUrl) : IRequest<string>;
