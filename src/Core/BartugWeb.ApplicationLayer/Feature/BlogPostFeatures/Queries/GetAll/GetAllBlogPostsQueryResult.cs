using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Queries.GetAll;

public class GetAllBlogPostsQueryResult : IRequestHandler<GetAllBlogPostsQuery, IEnumerable<BlogPost>>
{
    private readonly IBlogPostRepository _blogPostRepository;

    public GetAllBlogPostsQueryResult(IBlogPostRepository blogPostRepository)
    {
        _blogPostRepository = blogPostRepository;
    }

    public async Task<IEnumerable<BlogPost>> Handle(GetAllBlogPostsQuery request, CancellationToken cancellationToken)
    {
        return await _blogPostRepository.GetAllAsync(cancellationToken);
    }
}