using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.RemoveCommands;

public class RemoveSocialMediaCommandHandler : IRequestHandler<RemoveSocialMediaCommand, string>
{
    private readonly ISocialMediaRepository _socialMediaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSocialMediaCommandHandler(ISocialMediaRepository socialMediaRepository, IUnitOfWork unitOfWork)
    {
        _socialMediaRepository = socialMediaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveSocialMediaCommand request, CancellationToken cancellationToken)
    {
        var socialMedia = await _socialMediaRepository.GetByIdAsync(request.Id, cancellationToken);

        if (socialMedia is null)
            throw new Exception($"SocialMedia with id {request.Id} not found");

        _socialMediaRepository.Delete(socialMedia);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"SocialMedia with id {request.Id} has been removed successfully.";
    }
}
