using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.UpdateCommands;

public record UpdateExperienceCommand(
    string ExperienceId,
    string Company,
    string Position,
    string Location,
    DateTime StartDate,
    DateTime? EndDate
) : IRequest<string>;
