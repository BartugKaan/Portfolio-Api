using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.EducationFeatures.Commands.RemoveCommands;

public record RemoveEducationCommand(string EducationId) : IRequest<string>;
