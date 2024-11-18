using ProductAPI.Models.Domain; // Importing domain models, particularly the Product class

namespace ProductAPI.Repositories.Abstract
{
    /// <summary>
    /// Interface for Product repository operations.
    /// Provides a contract for CRUD operations and searching on the Product entity.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Adds or updates a Product record in the database.
        /// </summary>
        /// <param name="product">The Product object to be added or updated.</param>
        /// <returns>A boolean indicating success (true) or failure (false).</returns>
        bool AddUpdate(Product product);

        /// <summary>
        /// Deletes a Product record by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Product to delete.</param>
        /// <returns>A boolean indicating success (true) or failure (false).</returns>
        bool Delete(int id);

        /// <summary>
        /// Retrieves a single Product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Product to retrieve.</param>
        /// <returns>The Product object if found; otherwise, null.</returns>
        Product GetById(int id);

        /// <summary>
        /// Retrieves all Product records, optionally filtering by a search term.
        /// </summary>
        /// <param name="term">A search term to filter the results (default is an empty string).</param>
        /// <returns>An IEnumerable collection of Product objects matching the filter.</returns>
        IEnumerable<Product> GetAll(string term = "");
    }
}
