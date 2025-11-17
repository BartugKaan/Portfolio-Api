using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.UpdateCommands;

public record UpdateProjectCommand(
    string Id,
    IFormFile? ProjectImage,
    string Title,
    string Description,
    List<string> Keyword,
    string ProjectUrl) : IRequest<string>;
