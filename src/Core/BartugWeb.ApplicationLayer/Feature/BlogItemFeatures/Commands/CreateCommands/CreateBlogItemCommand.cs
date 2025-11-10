using BartugWeb.DomainLayer.Enums;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.CreateCommands;

public record CreateBlogItemCommand(
    string CoverImgUrl,
    string Title,
    string Description,
    BlogCategory BlogCategory) : IRequest<string>;
