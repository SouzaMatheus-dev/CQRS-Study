using _123Sales.Domain.Entities;

namespace _123Sales.Domain.Interfaces.Services
{
    /// <summary>
    /// Service interface for handling business logic related to sales.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Cancels a sale by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to cancel.</param>
        Task CancelSaleAsync(Guid id);

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="sale">The sale entity to create.</param>
        Task CreateSaleAsync(Sale sale);

        /// <summary>
        /// Retrieves a specific sale by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <returns>The sale entity if found, otherwise null.</returns>
        Task<Sale> GetSaleByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all sales.
        /// </summary>
        /// <returns>List of sales.</returns>
        Task<IEnumerable<Sale>> GetSalesAsync();

        /// <summary>
        /// Updates an existing sale.
        /// </summary>
        /// <param name="sale">The sale entity to update.</param>
        Task UpdateSaleAsync(Sale sale);
    }
}