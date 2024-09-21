using _123Sales.Domain.Entities;

namespace _123Sales.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for handling sale data operations.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Adds a new sale to the database.
        /// </summary>
        /// <param name="sale">The sale entity to add.</param>
        Task AddSaleAsync(Sale sale);

        /// <summary>
        /// Deletes a sale by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the sale to delete.</param>
        Task DeleteSaleAsync(Guid id);

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