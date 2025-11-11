using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.UpdateCommands;

public class UpdateEducationCommandHandler : IRequestHandler<UpdateEducationCommand, string>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateEducationCommandHandler(
        IEducationRepository educationRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _educationRepository = educationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
    {
        // 1. Education'ı bul
        var education = await _educationRepository.GetByIdAsync(request.EducationId, cancellationToken);
        if (education is null)
            throw new Exception($"Education with id {request.EducationId} not found");

        // 2. Command'dan entity'ye map et
        _mapper.Map(request, education);

        // 3. Update ve save
        _educationRepository.Update(education);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Education with id {request.EducationId} has been updated successfully.";
    }
}
