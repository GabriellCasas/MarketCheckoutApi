using MarketCheckout.Domain.Interfaces.Repository;
using MarketCheckout.Infrastructure.Persistence.Data;
using MarketCheckoutApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCheckout.Persistence.Data
{
    public class CartRepository : ICartRepository
    {
        readonly MarketCheckoutDbContext _context;
        public CartRepository(MarketCheckoutDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Cart entity, CancellationToken cancellationToken)
        {
            _context.Carts.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Delete(Cart entity)
        {
            _context.Carts.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Carts.FindAsync(id, cancellationToken);
        }

        public async Task<Cart?> GetByIdWithItens(int id, CancellationToken cancellationToken)
        {
            return await _context.Carts.Include(c => c.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Cart entity)
        {
            _context.Carts.Update(entity);
            _context.SaveChanges();
        }
    }
}
