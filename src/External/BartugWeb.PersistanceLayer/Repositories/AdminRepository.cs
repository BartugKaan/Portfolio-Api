using System;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BartugWeb.PersistanceLayer.Repositories;

public class AdminRepository : Repository<Admin>, IAdminRepository
{
  public AdminRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<Admin?> GetByEmailAsync(string email, CancellationToken cancellationToken)
  {
    return await _context.Set<Admin>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
  }

  public async Task<Admin?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
  {
    return await _context.Set<Admin>().FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
  }
}
