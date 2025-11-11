using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Queries.GetByAboutId;

public record GetExperiencesByAboutIdQuery(string AboutId) : IRequest<IEnumerable<Experience>>;
