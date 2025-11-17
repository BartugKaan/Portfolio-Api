using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.UpdateCommand;

public record UpdateBlogPostCommand(
    string Id,
    IFormFile? HeaderImage,
    string Title,
    string BlogContent,
    List<string> Keywords): IRequest<string>;