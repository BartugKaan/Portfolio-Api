using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Queries.GetByAboutId;

public record GetEducationsByAboutIdQuery(string AboutId) : IRequest<IEnumerable<Education>>;
