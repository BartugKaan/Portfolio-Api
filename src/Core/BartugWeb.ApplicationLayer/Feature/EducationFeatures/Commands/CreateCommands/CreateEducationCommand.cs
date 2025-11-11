using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.CreateCommands;

public record CreateEducationCommand(
    string AboutId,
    string Title,
    string SchoolName,
    int StartYear,
    int? EndYear
) : IRequest<string>;
