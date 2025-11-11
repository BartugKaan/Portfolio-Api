using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.CreateCommand;

public class CreateExperienceCommandHandler : IRequestHandler<CreateExperienceCommand, string>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IAboutRepository _aboutRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public CreateExperienceCommandHandler(IExperienceRepository experienceRepository, IAboutRepository aboutRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _experienceRepository = experienceRepository;
        _aboutRepository = aboutRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
    {
        var aboutExists = await _aboutRepository.ExistsAsync(request.AboutId, cancellationToken);
        if (!aboutExists)
            throw new Exception($"About with id {request.AboutId} does not exist");
        
        var experience = _mapper.Map<Experience>(request);
        
        await _experienceRepository.AddAsync(experience, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return experience.Id;
    }
}