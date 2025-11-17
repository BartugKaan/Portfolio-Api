using BartugWeb.DomainLayer.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.CreateCommands;

public record CreateBlogItemCommand(
    IFormFile CoverImage,
    string Title,
    string Description,
    BlogCategory BlogCategory) : IRequest<string>;
