using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.RemoveCommands;

public record RemoveStackCommand(string StackId) : IRequest<string>;
