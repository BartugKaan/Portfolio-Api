using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Queries.GetAll;

public class GetAllAboutsQueryHandler : IRequestHandler<GetAllAboutsQuery, IEnumerable<About>>
{
    private readonly IAboutRepository _aboutRepository;

    public GetAllAboutsQueryHandler(IAboutRepository aboutRepository)
    {
        _aboutRepository = aboutRepository;
    }

    public async Task<IEnumerable<About>> Handle(GetAllAboutsQuery request, CancellationToken cancellationToken)
    {
        return await _aboutRepository.GetAllAsync(cancellationToken);
    }
}