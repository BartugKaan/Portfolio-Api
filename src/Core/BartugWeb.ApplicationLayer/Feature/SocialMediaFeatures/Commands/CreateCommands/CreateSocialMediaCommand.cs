using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;

public record CreateSocialMediaCommand(
    string IconUrl,
    string LinkUrl) : IRequest<string>;
