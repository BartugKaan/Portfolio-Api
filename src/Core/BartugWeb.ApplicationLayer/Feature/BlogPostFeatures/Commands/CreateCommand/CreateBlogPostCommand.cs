using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.CreateCommand;

public record CreateBlogPostCommand(
        string HeaderImageUrl,
        string Title,
        string BlogContent,
        List<string> Keywords) : IRequest<string>;