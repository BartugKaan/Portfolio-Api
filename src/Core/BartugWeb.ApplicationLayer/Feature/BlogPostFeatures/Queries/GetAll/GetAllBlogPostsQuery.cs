using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Queries.GetAll;

public record GetAllBlogPostsQuery: IRequest<IEnumerable<BlogPost>>;