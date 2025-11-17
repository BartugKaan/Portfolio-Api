using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.UpdateCommands;

public record UpdateSocialMediaCommand(
    string Id,
    IFormFile? Icon,
    string LinkUrl) : IRequest<string>;
