using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.RemoveCommands;

public record RemoveBlogItemCommand(string Id) : IRequest<string>;
