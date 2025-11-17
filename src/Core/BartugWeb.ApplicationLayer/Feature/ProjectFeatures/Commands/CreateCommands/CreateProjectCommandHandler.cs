using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ProjectFeatures.Commands.CreateCommands;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, string>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public CreateProjectCommandHandler(IProjectRepository projectRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = _mapper.Map<Project>(request);

        if (request.ProjectImage is not null && request.ProjectImage.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.ProjectImage.FileName}";
            await using var stream = request.ProjectImage.OpenReadStream();
            var fileUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.ProjectImage.ContentType);
            project.ProjectImgUrl = fileUrl;
        }

        await _projectRepository.AddAsync(project, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
