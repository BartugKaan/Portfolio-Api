using System;

namespace BartugWeb.ApplicationLayer.Abstracts.IServices;

public interface IPasswordHasher
{
  string Hash(string password);
  bool Verify(string password, string hashedPassword);
}
