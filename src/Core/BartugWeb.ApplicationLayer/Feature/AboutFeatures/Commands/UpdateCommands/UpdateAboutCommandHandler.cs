using AutoMapper;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AboutFeatures.Commands.UpdateCommands;

public class UpdateAboutCommandHandler : IRequestHandler<UpdateAboutCommand, string>
{
    private readonly IAboutRepository _aboutRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAboutCommandHandler(IAboutRepository aboutRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _aboutRepository = aboutRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
    {
        var about = await _aboutRepository.GetByIdAsync(request.AboutId, cancellationToken);

        if (about is null)
            throw new Exception($"About with id {request.AboutId} not found");

        _mapper.Map(request, about);
        _aboutRepository.Update(about);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return $"About with id {request.AboutId} has been updated successfully.";
        
    }
}