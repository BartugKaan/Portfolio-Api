using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Queries.GetById;

public record GetBlogItemByIdQuery(string Id) : IRequest<BlogItem?>;
