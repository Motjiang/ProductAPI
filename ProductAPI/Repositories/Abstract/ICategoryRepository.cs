using ProductAPI.Models.Domain; // Importing the domain models, particularly the Category class

namespace ProductAPI.Repositories.Abstract
{
    /// <summary>
    /// Interface for Category repository operations. 
    /// Provides an abstraction for CRUD operations on the Category entity.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Adds or updates a Category record in the database.
        /// </summary>
        /// <param name="category">The Category object to be added or updated.</param>
        /// <returns>A boolean indicating success (true) or failure (false).</returns>
        bool AddUpdate(Category category);

        /// <summary>
        /// Deletes a Category record by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Category to delete.</param>
        /// <returns>A boolean indicating success (true) or failure (false).</returns>
        bool Delete(int id);

        /// <summary>
        /// Retrieves a single Category by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the Category to retrieve.</param>
        /// <returns>The Category object if found; otherwise, null.</returns>
        Category GetById(int id);

        /// <summary>
        /// Retrieves all Category records from the database.
        /// </summary>
        /// <returns>An IEnumerable collection of Category objects.</returns>
        IEnumerable<Category> GetAll();
    }
}
