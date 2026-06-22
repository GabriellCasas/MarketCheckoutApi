using MarketCheckout.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCheckout.Infrastructure.Persistence.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly MarketCheckoutDbContext _context;

        public BaseRepository(MarketCheckoutDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.AddAsync(entity, cancellationToken);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.FindAsync<T>(new object[] { id }, cancellationToken);            
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }   
    }
}