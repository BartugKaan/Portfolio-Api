using BartugWeb.DomainLayer.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.UpdateCommands;

public record UpdateBlogItemCommand(
    string Id,
    IFormFile? CoverImage,
    string Title,
    string Description,
    BlogCategory BlogCategory) : IRequest<string>;
