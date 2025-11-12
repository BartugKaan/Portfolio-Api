using System;
using BartugWeb.DomainLayer.Entities;

namespace BartugWeb.ApplicationLayer.Abstracts.IRepositories;

public interface IAdminRepository : IRepository<Admin>
{
  Task<Admin?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
  Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
