using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.RemoveCommands;

public class RemoveAboutCommandHandler : IRequestHandler<RemoveAboutCommand, string>
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAboutCommandHandler(IAboutRepository aboutRepository, IUnitOfWork unitOfWork)
    {
        _aboutRepository = aboutRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await _aboutRepository.GetByIdAsync(request.AboutId, cancellationToken);
        
        if (about is null)
            throw new Exception($"About with id {request.AboutId} not found");
        
        _aboutRepository.Delete(about);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return $"About with id {request.AboutId} has been removed successfully.";
    }
}