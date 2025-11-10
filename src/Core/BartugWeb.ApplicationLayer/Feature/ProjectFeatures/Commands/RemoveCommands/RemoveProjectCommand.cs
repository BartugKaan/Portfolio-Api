using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.RemoveCommands;

public record RemoveProjectCommand(string Id) : IRequest<string>;
