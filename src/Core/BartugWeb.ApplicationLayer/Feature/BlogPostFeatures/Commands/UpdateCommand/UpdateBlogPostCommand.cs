using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.UpdateCommand;

public record UpdateBlogPostCommand(
    string Id,
    string HeaderImageUrl,
    string Title,
    string BlogContent,
    List<string> Keywords): IRequest<string>;