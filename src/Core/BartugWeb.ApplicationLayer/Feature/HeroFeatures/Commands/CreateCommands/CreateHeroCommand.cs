using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.CreateCommands;

public record CreateHeroCommand(
    string HeroImageUrl,
    string Title,
    string Name,
    string JobTitles,
    string Description) : IRequest<string>;
