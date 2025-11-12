using System;

namespace BartugWeb.ApplicationLayer.Feature.AuthFeatures.DTOs;

public record LoginRequest(string Username, string Password);
