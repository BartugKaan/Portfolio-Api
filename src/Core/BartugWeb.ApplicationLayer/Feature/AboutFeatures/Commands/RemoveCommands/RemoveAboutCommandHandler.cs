using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.RemoveCommands;

public class RemoveAboutCommandHandler : IRequestHandler<RemoveAboutCommand, string>
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public RemoveAboutCommandHandler(IAboutRepository aboutRepository, IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _aboutRepository = aboutRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(RemoveAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await _aboutRepository.GetByIdAsync(request.AboutId, cancellationToken);
        
        if (about is null)
            throw new Exception($"About with id {request.AboutId} not found");

        if (!string.IsNullOrEmpty(about.ImageUrl))
        {
            var fileName = about.ImageUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(fileName);
        }
        
        _aboutRepository.Delete(about);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return $"About with id {request.AboutId} has been removed successfully.";
    }
}