using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Queries.GetAll;

public record GetAllAboutsQuery : IRequest<IEnumerable<About>>;