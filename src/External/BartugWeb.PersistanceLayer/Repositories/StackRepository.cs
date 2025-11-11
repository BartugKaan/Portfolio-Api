using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.DomainLayer.Entities;
using BartugWeb.PersistanceLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BartugWeb.PersistanceLayer.Repositories;

public class StackRepository : Repository<Stack>, IStackRepository
{
    public StackRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Stack>> GetByAboutIdAsync(string aboutId, CancellationToken cancellationToken)
    {
        return await _context.Set<Stack>()
            .Where(s => s.AboutId == aboutId)
            .OrderBy(s => s.Category)
            .ThenBy(s => s.Order)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Stack>> GetByCategoryAsync(string aboutId, string category, CancellationToken cancellationToken)
    {
        return await _context.Set<Stack>()
            .Where(s => s.AboutId == aboutId && s.Category == category)
            .OrderBy(s => s.Order)
            .ToListAsync(cancellationToken);
    }
}
