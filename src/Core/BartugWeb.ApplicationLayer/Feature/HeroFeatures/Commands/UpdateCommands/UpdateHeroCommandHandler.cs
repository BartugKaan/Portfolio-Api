using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.UpdateCommands;

public class UpdateHeroCommandHandler : IRequestHandler<UpdateHeroCommand, string>
{
    private readonly IHeroRepository _heroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public UpdateHeroCommandHandler(IHeroRepository heroRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _heroRepository = heroRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = await _heroRepository.GetByIdAsync(request.Id, cancellationToken);

        if (hero is null)
            throw new Exception($"Hero with id {request.Id} not found");

        string? newImageUrl = null;
        if (request.HeroImage is not null && request.HeroImage.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.HeroImage.FileName}";
            await using var stream = request.HeroImage.OpenReadStream();
            newImageUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.HeroImage.ContentType);

            if (!string.IsNullOrEmpty(hero.HeroImageUrl))
            {
                var oldFileName = hero.HeroImageUrl.Split('/').Last();
                await _fileStorageService.DeleteFileAsync(oldFileName);
            }
        }

        var updatedHero = _mapper.Map(request, hero);

        if (newImageUrl is not null)
        {
            updatedHero.HeroImageUrl = newImageUrl;
        }

        _heroRepository.Update(updatedHero);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Hero with id {request.Id} has been updated successfully.";
    }
}
