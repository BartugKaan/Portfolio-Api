using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetByAboutId;

public record GetStacksByAboutIdQuery(string AboutId) : IRequest<IEnumerable<Stack>>;
