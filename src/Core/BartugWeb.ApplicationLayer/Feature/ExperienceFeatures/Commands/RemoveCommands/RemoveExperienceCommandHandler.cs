using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.RemoveCommands;

public class RemoveExperienceCommandHandler : IRequestHandler<RemoveExperienceCommand, string>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveExperienceCommandHandler(
        IExperienceRepository experienceRepository,
        IUnitOfWork unitOfWork)
    {
        _experienceRepository = experienceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveExperienceCommand request, CancellationToken cancellationToken)
    {
        // 1. Experience'ı bul
        var experience = await _experienceRepository.GetByIdAsync(request.ExperienceId, cancellationToken);
        if (experience is null)
            throw new Exception($"Experience with id {request.ExperienceId} not found");

        // 2. Sil ve save
        _experienceRepository.Delete(experience);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Experience with id {request.ExperienceId} has been deleted successfully.";
    }
}
