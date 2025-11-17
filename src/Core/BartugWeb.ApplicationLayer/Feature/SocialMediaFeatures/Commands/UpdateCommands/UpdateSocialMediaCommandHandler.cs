using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
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

        var oldIconUrl = socialMedia.IconUrl;
        if (request.IconUrl != oldIconUrl && !string.IsNullOrEmpty(oldIconUrl))
        {
            var oldFileName = oldIconUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(oldFileName);
        }

        _mapper.Map(request, socialMedia);
        _socialMediaRepository.Update(socialMedia);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"SocialMedia with id {request.Id} has been updated successfully.";
    }
}
