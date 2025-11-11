using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetByAboutId;

public class GetStacksByAboutIdQueryHandler : IRequestHandler<GetStacksByAboutIdQuery, IEnumerable<Stack>>
{
    private readonly IStackRepository _stackRepository;

    public GetStacksByAboutIdQueryHandler(IStackRepository stackRepository)
    {
        _stackRepository = stackRepository;
    }

    public async Task<IEnumerable<Stack>> Handle(GetStacksByAboutIdQuery request, CancellationToken cancellationToken)
    {
        return await _stackRepository.GetByAboutIdAsync(request.AboutId, cancellationToken);
    }
}
