using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.CreateCommands;

public class CreateAboutCommandHandler : IRequestHandler<CreateAboutCommand, string>
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public CreateAboutCommandHandler(IAboutRepository aboutRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _aboutRepository = aboutRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = _mapper.Map<About>(request);

        if (request.Image is not null && request.Image.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.Image.FileName}";
            await using var stream = request.Image.OpenReadStream();
            var fileUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.Image.ContentType);
            about.ImageUrl = fileUrl;
        }

        await _aboutRepository.AddAsync(about, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return about.Id;
    }
}