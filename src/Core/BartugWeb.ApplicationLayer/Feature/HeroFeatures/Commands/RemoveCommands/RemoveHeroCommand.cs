using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.RemoveCommands;

public record RemoveHeroCommand(string Id) : IRequest<string>;
