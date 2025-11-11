using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.RemoveCommands;

public class RemoveStackCommandHandler : IRequestHandler<RemoveStackCommand, string>
{
    private readonly IStackRepository _stackRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveStackCommandHandler(
        IStackRepository stackRepository,
        IUnitOfWork unitOfWork)
    {
        _stackRepository = stackRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveStackCommand request, CancellationToken cancellationToken)
    {
        // 1. Stack'i bul
        var stack = await _stackRepository.GetByIdAsync(request.StackId, cancellationToken);
        if (stack is null)
            throw new Exception($"Stack with id {request.StackId} not found");

        // 2. Sil ve save
        _stackRepository.Delete(stack);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Stack with id {request.StackId} has been deleted successfully.";
    }
}
