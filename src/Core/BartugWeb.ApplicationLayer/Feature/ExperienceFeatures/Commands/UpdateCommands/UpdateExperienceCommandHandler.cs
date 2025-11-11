using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.UpdateCommands;

public class UpdateExperienceCommandHandler : IRequestHandler<UpdateExperienceCommand, string>
{
    private readonly IExperienceRepository _experienceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateExperienceCommandHandler(
        IExperienceRepository experienceRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _experienceRepository = experienceRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
    {
        // 1. Experience'ı bul
        var experience = await _experienceRepository.GetByIdAsync(request.ExperienceId, cancellationToken);
        if (experience is null)
            throw new Exception($"Experience with id {request.ExperienceId} not found");

        // 2. Command'dan entity'ye map et (sadece güncellenecek alanlar)
        _mapper.Map(request, experience);

        // 3. Update ve save
        _experienceRepository.Update(experience);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Experience with id {request.ExperienceId} has been updated successfully.";
    }
}
