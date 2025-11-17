using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.RemoveCommands;

public class RemoveHeroCommandHandler : IRequestHandler<RemoveHeroCommand, string>
{
    private readonly IHeroRepository _heroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public RemoveHeroCommandHandler(IHeroRepository heroRepository, IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _heroRepository = heroRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(RemoveHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = await _heroRepository.GetByIdAsync(request.Id, cancellationToken);

        if (hero is null)
            throw new Exception($"Hero with id {request.Id} not found");

        if (!string.IsNullOrEmpty(hero.HeroImageUrl))
        {
            var fileName = hero.HeroImageUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(fileName);
        }

        _heroRepository.Delete(hero);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Hero with id {request.Id} has been removed successfully.";
    }
}
