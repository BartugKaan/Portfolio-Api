using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.UpdateCommand;

public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, string>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public UpdateBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _blogPostRepository = blogPostRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if(blogPost is null)
            throw new Exception($"BlogPost with id {request.Id} not found");
        
        string? newImageUrl = null;
        if (request.HeaderImage is not null && request.HeaderImage.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.HeaderImage.FileName}";
            await using var stream = request.HeaderImage.OpenReadStream();
            newImageUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.HeaderImage.ContentType);

            if (!string.IsNullOrEmpty(blogPost.HeaderImageUrl))
            {
                var oldFileName = blogPost.HeaderImageUrl.Split('/').Last();
                await _fileStorageService.DeleteFileAsync(oldFileName);
            }
        }

        var updatedBlogPost = _mapper.Map(request, blogPost);

        if (newImageUrl is not null)
        {
            updatedBlogPost.HeaderImageUrl = newImageUrl;
        }
        
        _blogPostRepository.Update(updatedBlogPost);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return $"BlogPost with id {request.Id} has been updated successfully.";
    }
}