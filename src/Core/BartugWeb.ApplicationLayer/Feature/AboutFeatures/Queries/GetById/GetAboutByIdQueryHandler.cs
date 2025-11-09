using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Queries.GetById;

public class GetAboutByIdQueryHandler : IRequestHandler<GetAboutByIdQuery, About?>
{
    private readonly IAboutRepository _aboutRepository;

    public GetAboutByIdQueryHandler(IAboutRepository aboutRepository)
    {
        _aboutRepository = aboutRepository;
    }

    public async Task<About?> Handle(GetAboutByIdQuery request, CancellationToken cancellationToken)
    { 
        return await _aboutRepository.GetByIdAsync(request.AboutId, cancellationToken); 
    }
}