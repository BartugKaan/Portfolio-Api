using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.UpdateCommands;

public class UpdateGetInTouchCommandHandler : IRequestHandler<UpdateGetInTouchCommand, string>
{
    private readonly IGetInTouchRepository _getInTouchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateGetInTouchCommandHandler(IGetInTouchRepository getInTouchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _getInTouchRepository = getInTouchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateGetInTouchCommand request, CancellationToken cancellationToken)
    {
        var getInTouch = await _getInTouchRepository.GetByIdAsync(request.Id, cancellationToken);

        if (getInTouch is null)
            throw new Exception($"GetInTouch with id {request.Id} not found");

        _mapper.Map(request, getInTouch);
        _getInTouchRepository.Update(getInTouch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"GetInTouch with id {request.Id} has been updated successfully.";
    }
}
