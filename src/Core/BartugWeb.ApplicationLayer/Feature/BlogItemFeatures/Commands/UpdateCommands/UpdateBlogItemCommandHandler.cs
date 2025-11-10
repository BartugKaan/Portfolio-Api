using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.UpdateCommands;

public class UpdateBlogItemCommandHandler : IRequestHandler<UpdateBlogItemCommand, string>
{
    private readonly IBlogItemRepository _blogItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBlogItemCommandHandler(IBlogItemRepository blogItemRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _blogItemRepository = blogItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string> Handle(UpdateBlogItemCommand request, CancellationToken cancellationToken)
    {
        var blogItem = await _blogItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blogItem is null)
            throw new Exception($"BlogItem with id {request.Id} not found");

        _mapper.Map(request, blogItem);
        _blogItemRepository.Update(blogItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"BlogItem with id {request.Id} has been updated successfully.";
    }
}
