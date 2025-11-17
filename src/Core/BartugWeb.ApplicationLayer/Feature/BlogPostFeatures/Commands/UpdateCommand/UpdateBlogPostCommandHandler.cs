using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
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
        
        var oldImageUrl = blogPost.HeaderImageUrl;
        if (request.HeaderImageUrl != oldImageUrl && !string.IsNullOrEmpty(oldImageUrl))
        {
            var oldFileName = oldImageUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(oldFileName);
        }

        _mapper.Map(request, blogPost);
        _blogPostRepository.Update(blogPost);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return $"BlogPost with id {request.Id} has been updated successfully.";
    }
}