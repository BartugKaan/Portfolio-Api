using System;

namespace BartugWeb.ApplicationLayer.Abstracts.IServices;

public interface IJwtService
{
  string GenerateToken(string userId, string username, string email);
}
