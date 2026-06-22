using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Domain.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}