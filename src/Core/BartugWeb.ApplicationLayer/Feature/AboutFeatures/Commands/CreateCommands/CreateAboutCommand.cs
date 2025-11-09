using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;

public record CreateAboutCommand(
    string Description,
    string ImageUrl,
    List<string> Stacks,
    List<string> Educations,
    List<string> Experience) : IRequest<string>;