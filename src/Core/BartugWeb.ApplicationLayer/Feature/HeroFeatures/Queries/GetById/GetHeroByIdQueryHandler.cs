using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Queries.GetById;

public class GetHeroByIdQueryHandler : IRequestHandler<GetHeroByIdQuery, Hero?>
{
    private readonly IHeroRepository _heroRepository;

    public GetHeroByIdQueryHandler(IHeroRepository heroRepository)
    {
        _heroRepository = heroRepository;
    }

    public async Task<Hero?> Handle(GetHeroByIdQuery request, CancellationToken cancellationToken)
    {
        return await _heroRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
