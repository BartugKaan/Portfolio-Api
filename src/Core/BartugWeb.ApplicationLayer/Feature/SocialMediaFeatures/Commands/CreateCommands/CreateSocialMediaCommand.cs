using MediatR;
using Microsoft.AspNetCore.Http;

namespace BartugWeb.ApplicationLayer.Feature.SocialMediaFeatures.Commands.CreateCommands;

public record CreateSocialMediaCommand(
    IFormFile Icon,
    string LinkUrl) : IRequest<string>;
