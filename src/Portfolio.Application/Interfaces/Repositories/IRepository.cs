using System.Linq.Expressions;

namespace Portfolio.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interfaz genérica para operaciones CRUD básicas.
    /// T representa cualquier entidad
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);        
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<bool> UpdateAsync(Guid id, T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<long> CountAsync();
    }
}
