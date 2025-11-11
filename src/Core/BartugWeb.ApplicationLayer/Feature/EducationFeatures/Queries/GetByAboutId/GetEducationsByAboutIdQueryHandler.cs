using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Queries.GetByAboutId;

public class GetEducationsByAboutIdQueryHandler : IRequestHandler<GetEducationsByAboutIdQuery, IEnumerable<Education>>
{
    private readonly IEducationRepository _educationRepository;

    public GetEducationsByAboutIdQueryHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public async Task<IEnumerable<Education>> Handle(GetEducationsByAboutIdQuery request, CancellationToken cancellationToken)
    {
        return await _educationRepository.GetByAboutIdAsync(request.AboutId, cancellationToken);
    }
}
