using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Queries.GetAll;

public class GetAllBlogItemsQueryHandler : IRequestHandler<GetAllBlogItemsQuery, IEnumerable<BlogItem>>
{
    private readonly IBlogItemRepository _blogItemRepository;

    public GetAllBlogItemsQueryHandler(IBlogItemRepository blogItemRepository)
    {
        _blogItemRepository = blogItemRepository;
    }

    public async Task<IEnumerable<BlogItem>> Handle(GetAllBlogItemsQuery request, CancellationToken cancellationToken)
    {
        return await _blogItemRepository.GetAllAsync(cancellationToken);
    }
}
