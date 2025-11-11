using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.RemoveCommands;

public class RemoveEducationCommandHandler : IRequestHandler<RemoveEducationCommand, string>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveEducationCommandHandler(
        IEducationRepository educationRepository,
        IUnitOfWork unitOfWork)
    {
        _educationRepository = educationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveEducationCommand request, CancellationToken cancellationToken)
    {
        // 1. Education'ı bul
        var education = await _educationRepository.GetByIdAsync(request.EducationId, cancellationToken);
        if (education is null)
            throw new Exception($"Education with id {request.EducationId} not found");

        // 2. Sil ve save
        _educationRepository.Delete(education);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Education with id {request.EducationId} has been deleted successfully.";
    }
}
