using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;

public record CreateAboutCommand(
    string Description,
    IFormFile Image) : IRequest<string>;