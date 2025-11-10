using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.RemoveCommands;

public class RemoveHeroCommandHandler : IRequestHandler<RemoveHeroCommand, string>
{
    private readonly IHeroRepository _heroRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveHeroCommandHandler(IHeroRepository heroRepository, IUnitOfWork unitOfWork)
    {
        _heroRepository = heroRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = await _heroRepository.GetByIdAsync(request.Id, cancellationToken);

        if (hero is null)
            throw new Exception($"Hero with id {request.Id} not found");

        _heroRepository.Delete(hero);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Hero with id {request.Id} has been removed successfully.";
    }
}
