using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.UpdateCommands;

public record UpdateHeroCommand(
    string Id,
    IFormFile? HeroImage,
    string Title,
    string Name,
    string JobTitles,
    string Description) : IRequest<string>;
