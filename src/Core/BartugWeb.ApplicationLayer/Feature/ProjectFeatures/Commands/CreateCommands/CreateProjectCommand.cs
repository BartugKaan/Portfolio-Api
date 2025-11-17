using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.CreateCommands;

public record CreateProjectCommand(
    IFormFile ProjectImage,
    string Title,
    string Description,
    List<string> Keyword,
    string ProjectUrl) : IRequest<string>;
