using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetByCategory;

public class GetStacksByCategoryQueryHandler : IRequestHandler<GetStacksByCategoryQuery, IEnumerable<Stack>>
{
    private readonly IStackRepository _stackRepository;

    public GetStacksByCategoryQueryHandler(IStackRepository stackRepository)
    {
        _stackRepository = stackRepository;
    }

    public async Task<IEnumerable<Stack>> Handle(GetStacksByCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _stackRepository.GetByCategoryAsync(request.AboutId, request.Category, cancellationToken);
    }
}
