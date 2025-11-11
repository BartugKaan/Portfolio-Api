using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Queries.GetByAboutId;

public class GetExperiencesByAboutIdQueryHandler : IRequestHandler<GetExperiencesByAboutIdQuery, IEnumerable<Experience>>
{
    private readonly IExperienceRepository _experienceRepository;

    public GetExperiencesByAboutIdQueryHandler(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }

    public async Task<IEnumerable<Experience>> Handle(GetExperiencesByAboutIdQuery request, CancellationToken cancellationToken)
    {
        return await _experienceRepository.GetByAboutIdAsync(request.AboutId, cancellationToken);
    }
}
