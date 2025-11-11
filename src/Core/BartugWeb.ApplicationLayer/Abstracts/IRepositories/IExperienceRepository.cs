using BartugWeb.DomainLayer.Entities;

namespace BartugWeb.ApplicationLayer.Abstracts.IRepositories;

public interface IExperienceRepository : IRepository<Experience>
{
    Task<IEnumerable<Experience>> GetByAboutIdAsync(string aboutId, CancellationToken cancellationToken);
}