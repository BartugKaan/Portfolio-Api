using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetAll;

public record GetAllSocialMediaQuery : IRequest<IEnumerable<SocialMedia>>;
