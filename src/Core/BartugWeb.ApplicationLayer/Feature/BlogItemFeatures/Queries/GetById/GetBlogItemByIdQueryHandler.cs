using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Queries.GetById;

public class GetBlogItemByIdQueryHandler : IRequestHandler<GetBlogItemByIdQuery, BlogItem?>
{
    private readonly IBlogItemRepository _blogItemRepository;

    public GetBlogItemByIdQueryHandler(IBlogItemRepository blogItemRepository)
    {
        _blogItemRepository = blogItemRepository;
    }

    public async Task<BlogItem?> Handle(GetBlogItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await _blogItemRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
