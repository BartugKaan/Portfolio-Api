using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetByCategory;

public record GetStacksByCategoryQuery(string AboutId, string Category) : IRequest<IEnumerable<Stack>>;
