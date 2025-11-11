using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Queries.GetById;

public class GetExperienceByIdQueryHandler : IRequestHandler<GetExperienceByIdQuery, Experience>
{
    private readonly IExperienceRepository _experienceRepository;

    public GetExperienceByIdQueryHandler(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }

    public async Task<Experience> Handle(GetExperienceByIdQuery request, CancellationToken cancellationToken)
    {
        var experience = await _experienceRepository.GetByIdAsync(request.Id, cancellationToken);
        if (experience is null)
            throw new Exception($"Experience with id {request.Id} not found");

        return experience;
    }
}
