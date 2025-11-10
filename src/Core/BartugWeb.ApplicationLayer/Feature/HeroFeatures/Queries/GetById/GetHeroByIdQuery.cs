using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetById;

public record GetHeroByIdQuery(string Id) : IRequest<Hero?>;
