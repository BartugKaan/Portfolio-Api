using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;

public class UpdateAboutCommandHandler : IRequestHandler<UpdateAboutCommand, string>
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public UpdateAboutCommandHandler(IAboutRepository aboutRepository, IMapper mapper, IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _aboutRepository = aboutRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await _aboutRepository.GetByIdAsync(request.AboutId, cancellationToken);

        if (about is null)
            throw new Exception($"About with id {request.AboutId} not found");

        string? newImageUrl = null;
        if (request.Image is not null && request.Image.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.Image.FileName}";
            await using var stream = request.Image.OpenReadStream();
            newImageUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.Image.ContentType);

            if (!string.IsNullOrEmpty(about.ImageUrl))
            {
                var oldFileName = about.ImageUrl.Split('/').Last();
                await _fileStorageService.DeleteFileAsync(oldFileName);
            }
        }

        var updatedAbout = _mapper.Map(request, about);

        if (newImageUrl is not null)
        {
            updatedAbout.ImageUrl = newImageUrl;
        }
        
        _aboutRepository.Update(updatedAbout);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return $"About with id {request.AboutId} has been updated successfully.";
    }
}