using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.CreateCommand;

public record CreateBlogPostCommand(
        IFormFile HeaderImage,
        string Title,
        string BlogContent,
        List<string> Keywords) : IRequest<string>;