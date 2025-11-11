using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BartugWeb.PersistanceLayer.Repositories;

public class EducationRepository : Repository<Education>, IEducationRepository
{
    public EducationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Education>> GetByAboutIdAsync(string aboutId, CancellationToken cancellationToken)
    {
        return await _context.Set<Education>()
            .Where(e => e.AboutId == aboutId)
            .OrderByDescending(e => e.StartYear)
            .ToListAsync(cancellationToken);
    }
}