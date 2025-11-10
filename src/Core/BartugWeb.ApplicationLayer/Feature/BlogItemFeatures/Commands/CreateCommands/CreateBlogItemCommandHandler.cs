using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.CreateCommands;

public class CreateBlogItemCommandHandler : IRequestHandler<CreateBlogItemCommand, string>
{
    private readonly IBlogItemRepository _blogItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBlogItemCommandHandler(IBlogItemRepository blogItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _blogItemRepository = blogItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateBlogItemCommand request, CancellationToken cancellationToken)
    {
        var blogItem = _mapper.Map<BlogItem>(request);

        await _blogItemRepository.AddAsync(blogItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return blogItem.Id;
    }
}
