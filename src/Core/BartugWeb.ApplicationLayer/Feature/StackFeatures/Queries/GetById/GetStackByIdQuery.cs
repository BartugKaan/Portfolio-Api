using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetById;

public record GetStackByIdQuery(string Id) : IRequest<Stack>;
