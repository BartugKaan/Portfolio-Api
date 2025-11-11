using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.CreateCommands;

public record CreateStackCommand(
    string AboutId,
    string Category,
    string Technology,
    int Order
) : IRequest<string>;
