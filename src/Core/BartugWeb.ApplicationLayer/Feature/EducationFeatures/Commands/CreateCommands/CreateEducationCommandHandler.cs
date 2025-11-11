using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.CreateCommands;

public class CreateEducationCommandHandler : IRequestHandler<CreateEducationCommand, string>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IAboutRepository _aboutRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateEducationCommandHandler(
        IEducationRepository educationRepository,
        IAboutRepository aboutRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _educationRepository = educationRepository;
        _aboutRepository = aboutRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
    {
        // 1. About'un var olduğunu kontrol et
        var aboutExists = await _aboutRepository.ExistsAsync(request.AboutId, cancellationToken);
        if (!aboutExists)
            throw new Exception($"About with id {request.AboutId} not found");

        // 2. Command'ı Education entity'sine map et
        var education = _mapper.Map<Education>(request);

        // 3. Database'e ekle
        await _educationRepository.AddAsync(education, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return education.Id;
    }
}
