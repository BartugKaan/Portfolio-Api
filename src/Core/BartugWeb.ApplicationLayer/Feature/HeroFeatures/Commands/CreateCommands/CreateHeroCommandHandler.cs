using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.CreateCommands;

public class CreateHeroCommandHandler : IRequestHandler<CreateHeroCommand, string>
{
    private readonly IHeroRepository _heroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public CreateHeroCommandHandler(IHeroRepository heroRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _heroRepository = heroRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = _mapper.Map<Hero>(request);

        if (request.HeroImage is not null && request.HeroImage.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.HeroImage.FileName}";
            await using var stream = request.HeroImage.OpenReadStream();
            var fileUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.HeroImage.ContentType);
            hero.HeroImageUrl = fileUrl;
        }

        await _heroRepository.AddAsync(hero, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return hero.Id;
    }
}
