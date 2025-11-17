using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.CreateCommands;

public record CreateHeroCommand(
    IFormFile HeroImage,
    string Title,
    string Name,
    string JobTitles,
    string Description) : IRequest<string>;
