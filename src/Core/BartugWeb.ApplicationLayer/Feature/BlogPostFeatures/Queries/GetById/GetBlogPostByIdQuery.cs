using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Queries.GetById;

public record GetBlogPostByIdQuery(string id) : IRequest<BlogPost>;