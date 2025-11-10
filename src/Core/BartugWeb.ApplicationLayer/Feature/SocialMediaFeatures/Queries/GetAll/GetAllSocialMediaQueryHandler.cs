using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetAll;

public class GetAllSocialMediaQueryHandler : IRequestHandler<GetAllSocialMediaQuery, IEnumerable<SocialMedia>>
{
    private readonly ISocialMediaRepository _socialMediaRepository;

    public GetAllSocialMediaQueryHandler(ISocialMediaRepository socialMediaRepository)
    {
        _socialMediaRepository = socialMediaRepository;
    }

    public async Task<IEnumerable<SocialMedia>> Handle(GetAllSocialMediaQuery request, CancellationToken cancellationToken)
    {
        return await _socialMediaRepository.GetAllAsync(cancellationToken);
    }
}
