using BartugWeb.DomainLayer.Enums;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.UpdateCommands;

public record UpdateBlogItemCommand(
    string Id,
    string CoverImgUrl,
    string Title,
    string Description,
    BlogCategory BlogCategory) : IRequest<string>;
