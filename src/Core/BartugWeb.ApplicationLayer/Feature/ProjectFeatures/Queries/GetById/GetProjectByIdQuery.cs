using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Queries.GetById;

public record GetProjectByIdQuery(string Id) : IRequest<Project?>;
