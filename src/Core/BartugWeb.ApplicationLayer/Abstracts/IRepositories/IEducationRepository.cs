using BartugWeb.DomainLayer.Entities;

namespace BartugWeb.ApplicationLayer.Abstracts.IRepositories;

public interface IEducationRepository : IRepository<Education>
{
    Task<IEnumerable<Education>> GetByAboutIdAsync(string aboutId, CancellationToken cancellationToken);
}