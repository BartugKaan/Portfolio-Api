using BartugWeb.DomainLayer.Entities;

namespace BartugWeb.ApplicationLayer.Abstracts.IRepositories;

public interface IStackRepository : IRepository<Stack>
{
    Task<IEnumerable<Stack>> GetByAboutIdAsync(string aboutId, CancellationToken cancellationToken);
    Task<IEnumerable<Stack>> GetByCategoryAsync(string aboutId, string category, CancellationToken cancellationToken);
}
