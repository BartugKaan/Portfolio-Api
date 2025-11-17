using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.RemoveCommands;

public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, string>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public RemoveProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
            throw new Exception($"Project with id {request.Id} not found");

        if (!string.IsNullOrEmpty(project.ProjectImgUrl))
        {
            var fileName = project.ProjectImgUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(fileName);
        }

        _projectRepository.Delete(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Project with id {request.Id} has been removed successfully.";
    }
}
