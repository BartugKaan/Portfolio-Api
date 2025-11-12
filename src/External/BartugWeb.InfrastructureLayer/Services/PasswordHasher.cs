using System;
using BartugWeb.ApplicationLayer.Abstracts.IServices;

namespace BartugWeb.InfrastructureLayer.Services;

public class PasswordHasher : IPasswordHasher
{
  public string Hash(string password)
  {
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  public bool Verify(string password, string hashedPassword)
  {
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
}
