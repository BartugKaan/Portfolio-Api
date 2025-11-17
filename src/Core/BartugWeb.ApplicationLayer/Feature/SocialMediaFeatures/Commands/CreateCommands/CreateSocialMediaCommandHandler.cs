using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;

public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand, string>
{
    private readonly ISocialMediaRepository _socialMediaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public CreateSocialMediaCommandHandler(ISocialMediaRepository socialMediaRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _socialMediaRepository = socialMediaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
    {
        var socialMedia = _mapper.Map<SocialMedia>(request);

        if (request.Icon is not null && request.Icon.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.Icon.FileName}";
            await using var stream = request.Icon.OpenReadStream();
            var fileUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.Icon.ContentType);
            socialMedia.IconUrl = fileUrl;
        }

        await _socialMediaRepository.AddAsync(socialMedia, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return socialMedia.Id;
    }
}
