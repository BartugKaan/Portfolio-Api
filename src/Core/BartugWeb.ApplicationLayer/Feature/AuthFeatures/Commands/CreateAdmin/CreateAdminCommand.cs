using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AuthFeatures.Commands.CreateAdmin;

public record CreateAdminCommand(
    string Username,
    string Password
    ) : IRequest<string>;