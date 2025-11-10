using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.CreateCommands;

public record CreateProjectCommand(
    string ProjectImgUrl,
    string Title,
    string Description,
    List<string> Keyword,
    string ProjectUrl) : IRequest<string>;
