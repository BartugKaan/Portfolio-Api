using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetById;

public record GetSocialMediaByIdQuery(string Id) : IRequest<SocialMedia?>;
