using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.UpdateCommands;

public class UpdateBlogItemCommandHandler : IRequestHandler<UpdateBlogItemCommand, string>
{
    private readonly IBlogItemRepository _blogItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public UpdateBlogItemCommandHandler(IBlogItemRepository blogItemRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _blogItemRepository = blogItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(UpdateBlogItemCommand request, CancellationToken cancellationToken)
    {
        var blogItem = await _blogItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blogItem is null)
            throw new Exception($"BlogItem with id {request.Id} not found");

        string? newImageUrl = null;
        if (request.CoverImage is not null && request.CoverImage.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.CoverImage.FileName}";
            await using var stream = request.CoverImage.OpenReadStream();
            newImageUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.CoverImage.ContentType);

            if (!string.IsNullOrEmpty(blogItem.CoverImgUrl))
            {
                var oldFileName = blogItem.CoverImgUrl.Split('/').Last();
                await _fileStorageService.DeleteFileAsync(oldFileName);
            }
        }

        var updatedBlogItem = _mapper.Map(request, blogItem);

        if (newImageUrl is not null)
        {
            updatedBlogItem.CoverImgUrl = newImageUrl;
        }

        _blogItemRepository.Update(updatedBlogItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"BlogItem with id {request.Id} has been updated successfully.";
    }
}
