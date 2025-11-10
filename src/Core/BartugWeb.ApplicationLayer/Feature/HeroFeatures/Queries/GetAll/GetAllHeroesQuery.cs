using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetAll;

public record GetAllHeroesQuery : IRequest<IEnumerable<Hero>>;
