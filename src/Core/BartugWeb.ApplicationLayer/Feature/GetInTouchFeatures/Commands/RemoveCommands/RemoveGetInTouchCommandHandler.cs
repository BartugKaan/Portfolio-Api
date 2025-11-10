using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.RemoveCommands;

public class RemoveGetInTouchCommandHandler : IRequestHandler<RemoveGetInTouchCommand, string>
{
    private readonly IGetInTouchRepository _getInTouchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveGetInTouchCommandHandler(IGetInTouchRepository getInTouchRepository, IUnitOfWork unitOfWork)
    {
        _getInTouchRepository = getInTouchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveGetInTouchCommand request, CancellationToken cancellationToken)
    {
        var getInTouch = await _getInTouchRepository.GetByIdAsync(request.Id, cancellationToken);

        if (getInTouch is null)
            throw new Exception($"GetInTouch with id {request.Id} not found");

        _getInTouchRepository.Delete(getInTouch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"GetInTouch with id {request.Id} has been removed successfully.";
    }
}
