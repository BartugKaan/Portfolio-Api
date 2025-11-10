using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Queries.GetAll;

public record GetAllProjectsQuery : IRequest<IEnumerable<Project>>;
