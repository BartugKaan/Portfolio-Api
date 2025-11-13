// src/Core/BartugWeb.ApplicationLayer/Options/JwtSettings.cs

namespace BartugWeb.ApplicationLayer.Options; // Namespace güncellendi

public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
    public int ExpirationMinutes { get; init; }
}
