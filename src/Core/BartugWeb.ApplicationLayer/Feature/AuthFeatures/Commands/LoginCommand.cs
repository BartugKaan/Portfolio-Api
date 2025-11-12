using System;
using BartugWeb.ApplicationLayer.Feature.AuthFeatures.DTOs;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AuthFeatures.Commands;

public record LoginCommand(string Username, string Password) : IRequest<LoginResponse>;
