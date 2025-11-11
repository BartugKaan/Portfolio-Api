using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BartugWeb.PersistanceLayer.Repositories;

public class ExperienceRepository : Repository<Experience>, IExperienceRepository
{
    public ExperienceRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Experience>> GetByAboutIdAsync(string aboutId, CancellationToken cancellationToken)
    {
        return await _context.Set<Experience>()
            .Where(e => e.AboutId == aboutId)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync(cancellationToken);
    }
}