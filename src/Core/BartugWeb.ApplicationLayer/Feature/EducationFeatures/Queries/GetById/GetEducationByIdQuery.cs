using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Queries.GetById;

public record GetEducationByIdQuery(string Id) : IRequest<Education>;
