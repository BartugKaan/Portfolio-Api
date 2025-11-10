using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.HeroFeatures.Commands.CreateCommands;

public class CreateHeroCommandHandler : IRequestHandler<CreateHeroCommand, string>
{
    private readonly IHeroRepository _heroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateHeroCommandHandler(IHeroRepository heroRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _heroRepository = heroRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = _mapper.Map<Hero>(request);

        await _heroRepository.AddAsync(hero, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return hero.Id;
    }
}
