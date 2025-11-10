using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Queries.GetAll;

public class GetAllGetInTouchQueryHandler : IRequestHandler<GetAllGetInTouchQuery, IEnumerable<GetInTouch>>
{
    private readonly IGetInTouchRepository _getInTouchRepository;

    public GetAllGetInTouchQueryHandler(IGetInTouchRepository getInTouchRepository)
    {
        _getInTouchRepository = getInTouchRepository;
    }

    public async Task<IEnumerable<GetInTouch>> Handle(GetAllGetInTouchQuery request, CancellationToken cancellationToken)
    {
        return await _getInTouchRepository.GetAllAsync(cancellationToken);
    }
}
