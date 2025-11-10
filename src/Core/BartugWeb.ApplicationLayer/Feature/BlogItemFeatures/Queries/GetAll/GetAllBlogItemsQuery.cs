using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Queries.GetAll;

public record GetAllBlogItemsQuery : IRequest<IEnumerable<BlogItem>>;
