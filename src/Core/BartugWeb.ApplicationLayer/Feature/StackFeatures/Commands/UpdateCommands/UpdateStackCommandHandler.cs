using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.UpdateCommands;

public class UpdateStackCommandHandler : IRequestHandler<UpdateStackCommand, string>
{
    private readonly IStackRepository _stackRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateStackCommandHandler(
        IStackRepository stackRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _stackRepository = stackRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateStackCommand request, CancellationToken cancellationToken)
    {
        // 1. Stack'i bul
        var stack = await _stackRepository.GetByIdAsync(request.StackId, cancellationToken);
        if (stack is null)
            throw new Exception($"Stack with id {request.StackId} not found");

        // 2. Command'dan entity'ye map et
        _mapper.Map(request, stack);

        // 3. Update ve save
        _stackRepository.Update(stack);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Stack with id {request.StackId} has been updated successfully.";
    }
}
