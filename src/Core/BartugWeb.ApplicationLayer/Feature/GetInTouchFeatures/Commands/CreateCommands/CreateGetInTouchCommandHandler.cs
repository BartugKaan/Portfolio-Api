using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.GetInTouchFeatures.Commands.CreateCommands;

public class CreateGetInTouchCommandHandler : IRequestHandler<CreateGetInTouchCommand, string>
{
    private readonly IGetInTouchRepository _getInTouchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateGetInTouchCommandHandler(IGetInTouchRepository getInTouchRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _getInTouchRepository = getInTouchRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateGetInTouchCommand request, CancellationToken cancellationToken)
    {
        var getInTouch = _mapper.Map<GetInTouch>(request);

        await _getInTouchRepository.AddAsync(getInTouch, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return getInTouch.Id;
    }
}
