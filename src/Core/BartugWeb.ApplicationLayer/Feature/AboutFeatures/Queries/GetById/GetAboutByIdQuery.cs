using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Queries.GetById;

public record GetAboutByIdQuery(string AboutId) : IRequest<About?>;