using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;

public record UpdateAboutCommand(
    string AboutId,
    string Description,
    string ImageUrl,
    List<string> Stacks,
    List<string> Educations,
    List<string> Experience) : IRequest<string>;