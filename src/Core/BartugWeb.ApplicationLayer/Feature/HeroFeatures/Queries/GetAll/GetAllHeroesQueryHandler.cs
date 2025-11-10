using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetAll;

public class GetAllHeroesQueryHandler : IRequestHandler<GetAllHeroesQuery, IEnumerable<Hero>>
{
    private readonly IHeroRepository _heroRepository;

    public GetAllHeroesQueryHandler(IHeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }

    public async Task<IEnumerable<Hero>> Handle(GetAllHeroesQuery request, CancellationToken cancellationToken)
    {
        return await _heroRepository.GetAllAsync(cancellationToken);
    }
}
