using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;

public record UpdateAboutCommand(
    string AboutId,
    string Description,
    IFormFile? Image) : IRequest<string>;