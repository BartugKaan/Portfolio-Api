using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.DeleteCommand;

public record RemoveBlogPostCommand(string Id) : IRequest<string>;