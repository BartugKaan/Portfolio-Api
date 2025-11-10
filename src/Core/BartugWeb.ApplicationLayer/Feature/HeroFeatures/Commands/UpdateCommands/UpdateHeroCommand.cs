using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.UpdateCommands;

public record UpdateHeroCommand(
    string Id,
    string HeroImageUrl,
    string Title,
    string Name,
    string JobTitles,
    string Description) : IRequest<string>;
