using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Queries.GetById;

public record GetGetInTouchByIdQuery(string Id) : IRequest<GetInTouch?>;
