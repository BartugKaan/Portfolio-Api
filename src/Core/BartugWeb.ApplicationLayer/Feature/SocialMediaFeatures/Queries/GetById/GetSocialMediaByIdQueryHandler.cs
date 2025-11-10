using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Queries.GetById;

public class GetSocialMediaByIdQueryHandler : IRequestHandler<GetSocialMediaByIdQuery, SocialMedia?>
{
    private readonly ISocialMediaRepository _socialMediaRepository;

    public GetSocialMediaByIdQueryHandler(ISocialMediaRepository socialMediaRepository)
    {
        _socialMediaRepository = socialMediaRepository;
    }

    public async Task<SocialMedia?> Handle(GetSocialMediaByIdQuery request, CancellationToken cancellationToken)
    {
        return await _socialMediaRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
