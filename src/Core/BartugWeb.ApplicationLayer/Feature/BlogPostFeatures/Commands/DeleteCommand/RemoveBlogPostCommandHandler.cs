using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.DeleteCommand;

public class RemoveBlogPostCommandHandler : IRequestHandler<RemoveBlogPostCommand, string>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUnitOfWork _unitOfWork;


    public RemoveBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork)
    {
        _blogPostRepository = blogPostRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = _blogPostRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blogPost is null)
            throw new Exception($"Blog Post with id {request.Id} not found"); 
        
        _blogPostRepository.Delete(blogPost.Result);
        _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return $"About with id {request.Id} has been removed successfully.";
        
    }
}