using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.BlogPostFeatures.Commands.CreateCommand;

public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, string>
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public CreateBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _blogPostRepository = blogPostRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<string> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var blogPost = _mapper.Map<BlogPost>(request);

        if (request.HeaderImage is not null && request.HeaderImage.Length > 0)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{request.HeaderImage.FileName}";
            await using var stream = request.HeaderImage.OpenReadStream();
            var fileUrl = await _fileStorageService.UploadFileAsync(stream, uniqueFileName, request.HeaderImage.ContentType);
            blogPost.HeaderImageUrl = fileUrl;
        }

        await _blogPostRepository.AddAsync(blogPost, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return blogPost.Id;
    }
}