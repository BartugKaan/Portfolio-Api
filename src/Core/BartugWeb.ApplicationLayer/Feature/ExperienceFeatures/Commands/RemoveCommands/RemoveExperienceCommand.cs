using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.ExperienceFeatures.Commands.RemoveCommands;

public record RemoveExperienceCommand(string ExperienceId) : IRequest<string>;
