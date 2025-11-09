using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;

public class CreateAboutCommandHandler : IRequestHandler<CreateAboutCommand, string>
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateAboutCommandHandler(IAboutRepository aboutRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _aboutRepository = aboutRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = _mapper.Map<About>(request);

        await _aboutRepository.AddAsync(about, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return about.Id;
    }
}