using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.RemoveCommands;

public class RemoveBlogItemCommandHandler : IRequestHandler<RemoveBlogItemCommand, string>
{
    private readonly IBlogItemRepository _blogItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveBlogItemCommandHandler(IBlogItemRepository blogItemRepository, IUnitOfWork unitOfWork)
    {
        _blogItemRepository = blogItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RemoveBlogItemCommand request, CancellationToken cancellationToken)
    {
        var blogItem = await _blogItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (blogItem is null)
            throw new Exception($"BlogItem with id {request.Id} not found");

        _blogItemRepository.Delete(blogItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"BlogItem with id {request.Id} has been removed successfully.";
    }
}
