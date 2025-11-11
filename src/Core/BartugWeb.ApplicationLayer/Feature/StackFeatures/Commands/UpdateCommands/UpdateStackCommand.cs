using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.UpdateCommands;

public record UpdateStackCommand(
    string StackId,
    string Category,
    string Technology,
    int Order
) : IRequest<string>;
