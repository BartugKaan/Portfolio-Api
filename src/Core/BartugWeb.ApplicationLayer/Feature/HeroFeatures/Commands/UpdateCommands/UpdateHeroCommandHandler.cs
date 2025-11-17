using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
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

        var oldImageUrl = hero.HeroImageUrl;
        if (request.HeroImageUrl != oldImageUrl && !string.IsNullOrEmpty(oldImageUrl))
        {
            var oldFileName = oldImageUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(oldFileName);
        }

        _mapper.Map(request, hero);
        _heroRepository.Update(hero);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Hero with id {request.Id} has been updated successfully.";
    }
}
