using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.UpdateCommands;

public record UpdateProjectCommand(
    string Id,
    string ProjectImgUrl,
    string Title,
    string Description,
    List<string> Keyword,
    string ProjectUrl) : IRequest<string>;
