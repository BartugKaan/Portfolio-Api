using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.RemoveCommands;

public record RemoveAboutCommand(string AboutId) : IRequest<string>;