using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.DeleteCommand;

public class RemoveBlogPostCommandHandler : IRequestHandler<RemoveBlogPostCommand, string>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;


    public RemoveBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _blogPostRepository = blogPostRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(RemoveBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = await _blogPostRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blogPost is null)
            throw new Exception($"Blog Post with id {request.Id} not found");

        if (!string.IsNullOrEmpty(blogPost.HeaderImageUrl))
        {
            var fileName = blogPost.HeaderImageUrl.Split('/').Last();
            await _fileStorageService.DeleteFileAsync(fileName);
        }

        _blogPostRepository.Delete(blogPost);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"About with id {request.Id} has been removed successfully.";

    }
}