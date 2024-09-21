using _123Sales.Domain.Entities;
using _123Sales.Domain.Interfaces.Repositories;
using _123Sales.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace _123Sales.Infra.Repositories
{
    /// <summary>
    /// Repository implementation for handling sale data operations.
    /// </summary>
    public class SaleRepository : ISaleRepository
    {
        private readonly SalesDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleRepository"/> class.
        /// </summary>
        /// <param name="context">The DbContext for accessing the database.</param>
        public SaleRepository(SalesDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task AddSaleAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteSaleAsync(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        /// <inheritdoc/>
        public async Task<Sale> GetSaleByIdAsync(Guid id)
        {
            return await _context.Sales.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Sale>> GetSalesAsync()
        {
            return await _context.Sales.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateSaleAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }
    }
}