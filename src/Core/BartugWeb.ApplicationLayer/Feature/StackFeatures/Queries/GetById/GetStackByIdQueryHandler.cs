using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Queries.GetById;

public class GetStackByIdQueryHandler : IRequestHandler<GetStackByIdQuery, Stack>
{
    private readonly IStackRepository _stackRepository;

    public GetStackByIdQueryHandler(IStackRepository stackRepository)
    {
        _stackRepository = stackRepository;
    }

    public async Task<Stack> Handle(GetStackByIdQuery request, CancellationToken cancellationToken)
    {
        var stack = await _stackRepository.GetByIdAsync(request.Id, cancellationToken);
        if (stack is null)
            throw new Exception($"Stack with id {request.Id} not found");

        return stack;
    }
}
