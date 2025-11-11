using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.UpdateCommands;

public record UpdateEducationCommand(
    string EducationId,
    string Title,
    string SchoolName,
    int StartYear,
    int? EndYear
) : IRequest<string>;
