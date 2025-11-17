using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogItemFeatures.Commands.CreateCommands;

public class CreateBlogItemCommandHandler : IRequestHandler<CreateBlogItemCommand, string>
{
    private readonly IBlogItemRepository _blogItemRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public CreateBlogItemCommandHandler(IBlogItemRepository blogItemRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _blogItemRepository = blogItemRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(CreateBlogItemCommand request, CancellationToken cancellationToken)
    {
        var blogItem = _mapper.Map<BlogItem>(request);

        if (request.CoverImage is not null && request.CoverImage.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.CoverImage.FileName}";
            await using var stream = request.CoverImage.OpenReadStream();
            var fileUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.CoverImage.ContentType);
            blogItem.CoverImgUrl = fileUrl;
        }

        await _blogItemRepository.AddAsync(blogItem, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return blogItem.Id;
    }
}
