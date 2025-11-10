using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;

public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand, string>
{
    private readonly ISocialMediaRepository _socialMediaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSocialMediaCommandHandler(ISocialMediaRepository socialMediaRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _socialMediaRepository = socialMediaRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
    {
        var socialMedia = await _socialMediaRepository.GetByIdAsync(request.Id, cancellationToken);

        if (socialMedia is null)
            throw new Exception($"SocialMedia with id {request.Id} not found");

        _mapper.Map(request, socialMedia);
        _socialMediaRepository.Update(socialMedia);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"SocialMedia with id {request.Id} has been updated successfully.";
    }
}
