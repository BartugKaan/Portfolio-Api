using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Queries.GetById;

public class GetBlogPostByIdQueryHandler : IRequestHandler<GetBlogPostByIdQuery, BlogPost>
{
    private readonly IBlogPostRepository _blogPostRepositor;

    public GetBlogPostByIdQueryHandler(IBlogPostRepository blogPostRepositor)
    {
        _blogPostRepositor = blogPostRepositor;
    }

    public async Task<BlogPost> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await _blogPostRepositor.GetByIdAsync(request.id, cancellationToken);
        
        if (blog is null)
            throw new Exception($"BlogPost with id {request.id} not found");
        
        return blog;
    }
}