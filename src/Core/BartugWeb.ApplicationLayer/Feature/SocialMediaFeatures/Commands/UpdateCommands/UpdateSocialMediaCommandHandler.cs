using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;

public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand, string>
{
    private readonly ISocialMediaRepository _socialMediaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public UpdateSocialMediaCommandHandler(ISocialMediaRepository socialMediaRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _socialMediaRepository = socialMediaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
    {
        var socialMedia = await _socialMediaRepository.GetByIdAsync(request.Id, cancellationToken);

        if (socialMedia is null)
            throw new Exception($"SocialMedia with id {request.Id} not found");

        string? newImageUrl = null;
        if (request.Icon is not null && request.Icon.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.Icon.FileName}";
            await using var stream = request.Icon.OpenReadStream();
            newImageUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.Icon.ContentType);

            if (!string.IsNullOrEmpty(socialMedia.IconUrl))
            {
                var oldFileName = socialMedia.IconUrl.Split('/').Last();
                await _fileStorageService.DeleteFileAsync(oldFileName);
            }
        }

        var updatedSocialMedia = _mapper.Map(request, socialMedia);

        if (newImageUrl is not null)
        {
            updatedSocialMedia.IconUrl = newImageUrl;
        }

        _socialMediaRepository.Update(updatedSocialMedia);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"SocialMedia with id {request.Id} has been updated successfully.";
    }
}
