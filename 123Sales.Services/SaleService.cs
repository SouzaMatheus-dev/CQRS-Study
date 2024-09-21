using _123Sales.Domain.Entities;
using _123Sales.Domain.Interfaces.Repositories;
using _123Sales.Domain.Interfaces.Services;

namespace _123Sales.Services;

/// <summary>
/// Service implementation for handling business logic related to sales.
/// </summary>
public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleService"/> class.
    /// </summary>
    /// <param name="saleRepository">The repository for accessing sale data.</param>
    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <inheritdoc/>
    public async Task CancelSaleAsync(Guid id)
    {
        var sale = await _saleRepository.GetSaleByIdAsync(id);
        if (sale != null)
        {
            sale.IsCancelled = true;
            await _saleRepository.UpdateSaleAsync(sale);
        }
    }

    /// <inheritdoc/>
    public async Task CreateSaleAsync(Sale sale)
    {
        // Business logic here
        await _saleRepository.AddSaleAsync(sale);
    }

    /// <inheritdoc/>
    public async Task<Sale> GetSaleByIdAsync(Guid id)
    {
        return await _saleRepository.GetSaleByIdAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Sale>> GetSalesAsync()
    {
        return await _saleRepository.GetSalesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateSaleAsync(Sale sale)
    {
        // Business logic here
        await _saleRepository.UpdateSaleAsync(sale);
    }
}