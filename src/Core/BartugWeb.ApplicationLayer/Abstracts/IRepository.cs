using BartugWeb.DomainLayer.Abstracts;

namespace BartugWeb.ApplicationLayer.Abstracts;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> AddAsync(T entity , CancellationToken cancellationToken);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> ExistsAsync(string id, CancellationToken cancellationToken);
    
}