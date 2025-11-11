using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.CreateCommand;

public record CreateExperienceCommand(
    string AboutId,
    string Company,
    string Position,
    string Location,
    DateTime StartDate,
    DateTime? EndDate) : IRequest<string>;