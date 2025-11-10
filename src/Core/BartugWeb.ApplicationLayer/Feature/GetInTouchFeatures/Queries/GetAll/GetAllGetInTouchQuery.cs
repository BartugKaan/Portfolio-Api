using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Queries.GetAll;

public record GetAllGetInTouchQuery : IRequest<IEnumerable<GetInTouch>>;
