using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Queries.GetById;

public record GetExperienceByIdQuery(string Id) : IRequest<Experience>;
