using System;

namespace BartugWeb.ApplicationLayer.Feature.AuthFeatures.DTOs;

public record LoginResponse(string Token, DateTime ExpiresAt);
