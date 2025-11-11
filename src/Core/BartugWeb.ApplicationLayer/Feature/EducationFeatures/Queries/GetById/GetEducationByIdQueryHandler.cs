using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Queries.GetById;

public class GetEducationByIdQueryHandler : IRequestHandler<GetEducationByIdQuery, Education>
{
    private readonly IEducationRepository _educationRepository;

    public GetEducationByIdQueryHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public async Task<Education> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
    {
        var education = await _educationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (education is null)
            throw new Exception($"Education with id {request.Id} not found");

        return education;
    }
}
