using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;

public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand, string>
{
    private readonly ISocialMediaRepository _socialMediaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSocialMediaCommandHandler(ISocialMediaRepository socialMediaRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _socialMediaRepository = socialMediaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
    {
        var socialMedia = _mapper.Map<SocialMedia>(request);

        await _socialMediaRepository.AddAsync(socialMedia, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return socialMedia.Id;
    }
}
