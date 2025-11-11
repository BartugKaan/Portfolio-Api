using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.StackFeatures.Commands.CreateCommands;

public class CreateStackCommandHandler : IRequestHandler<CreateStackCommand, string>
{
    private readonly IStackRepository _stackRepository;
    private readonly IAboutRepository _aboutRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateStackCommandHandler(
        IStackRepository stackRepository,
        IAboutRepository aboutRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _stackRepository = stackRepository;
        _aboutRepository = aboutRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateStackCommand request, CancellationToken cancellationToken)
    {
        // 1. About'un var olduğunu kontrol et
        var aboutExists = await _aboutRepository.ExistsAsync(request.AboutId, cancellationToken);
        if (!aboutExists)
            throw new Exception($"About with id {request.AboutId} not found");

        // 2. Command'ı Stack entity'sine map et
        var stack = _mapper.Map<Stack>(request);

        // 3. Database'e ekle
        await _stackRepository.AddAsync(stack, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return stack.Id;
    }
}
