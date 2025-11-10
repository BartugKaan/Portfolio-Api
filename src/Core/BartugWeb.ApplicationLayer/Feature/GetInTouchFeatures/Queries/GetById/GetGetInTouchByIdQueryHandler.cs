using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Queries.GetById;

public class GetGetInTouchByIdQueryHandler : IRequestHandler<GetGetInTouchByIdQuery, GetInTouch?>
{
    private readonly IGetInTouchRepository _getInTouchRepository;

    public GetGetInTouchByIdQueryHandler(IGetInTouchRepository getInTouchRepository)
    {
        _getInTouchRepository = getInTouchRepository;
    }

    public async Task<GetInTouch?> Handle(GetGetInTouchByIdQuery request, CancellationToken cancellationToken)
    {
        return await _getInTouchRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
