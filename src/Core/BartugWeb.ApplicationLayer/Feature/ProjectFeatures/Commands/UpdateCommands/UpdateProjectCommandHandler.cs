using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities; // Add this if Project entity is not found
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.UpdateCommands;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, string>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public UpdateProjectCommandHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IFileStorageService fileStorageService)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

        if (project is null)
            throw new Exception($"Project with id {request.Id} not found");

        string? newImageUrl = null;
        if (request.ProjectImage is not null && request.ProjectImage.Length > 0)
        {
            // Upload the new image first
            var uniqueFileName = $"{Guid.NewGuid()}_{request.ProjectImage.FileName}";
            await using var stream = request.ProjectImage.OpenReadStream();
            newImageUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.ProjectImage.ContentType);

            // If upload is successful and there was an old image, delete the old one
            if (!string.IsNullOrEmpty(project.ProjectImgUrl))
            {
                var oldFileName = project.ProjectImgUrl.Split('/').Last();
                await _fileStorageService.DeleteFileAsync(oldFileName);
            }
        }

        // Map the request to the entity
        var updatedProject = _mapper.Map(request, project);

        // If a new image was uploaded, make sure its URL is set on the updated entity
        if (newImageUrl is not null)
        {
            updatedProject.ProjectImgUrl = newImageUrl;
        }

        _projectRepository.Update(updatedProject);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Project with id {request.Id} has been updated successfully.";
    }
}
