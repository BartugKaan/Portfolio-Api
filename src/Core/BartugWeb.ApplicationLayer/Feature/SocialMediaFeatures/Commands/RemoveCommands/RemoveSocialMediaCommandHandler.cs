using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.RemoveCommands;

public class RemoveSocialMediaCommandHandler : IRequestHandler<RemoveSocialMediaCommand, string>
{
    private readonly ISocialMediaRepository _socialMediaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public RemoveSocialMediaCommandHandler(ISocialMediaRepository socialMediaRepository, IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _socialMediaRepository = socialMediaRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(RemoveSocialMediaCommand request, CancellationToken cancellationToken)
    {
        var socialMedia = await _socialMediaRepository.GetByIdAsync(request.Id, cancellationToken);

        if (socialMedia is null)
            throw new Exception($"SocialMedia with id {request.Id} not found");

        if (!string.IsNullOrEmpty(socialMedia.IconUrl))
        {
            var fileName = socialMedia.IconUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(fileName);
        }

        _socialMediaRepository.Delete(socialMedia);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"SocialMedia with id {request.Id} has been removed successfully.";
    }
}
