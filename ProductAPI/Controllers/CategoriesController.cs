using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Domain; // Importing domain models for the Category entity
using ProductAPI.Models.DTOs; // Importing DTOs for structured responses
using ProductAPI.Repositories.Abstract; // Importing the repository interface for category operations

namespace ProductAPI.Controllers
{
    // Define the base route for this controller as "api/categories"
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        // Constructor with dependency injection for the repository interface
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Fetches all categories from the database.
        /// </summary>
        /// <returns>A list of categories.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            // Retrieve all categories using the repository
            var data = _categoryRepository.GetAll();
            return Ok(data); // Return HTTP 200 with the retrieved data
        }

        /// <summary>
        /// Fetches a single category by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>The category data or an appropriate response.</returns>
        [HttpGet("{id}")] // The route for this method is "api/categories/{id}"
        public IActionResult GetById(int id)
        {
            // Retrieve the category using its ID
            var data = _categoryRepository.GetById(id);
            return Ok(data); // Return HTTP 200 with the category data
        }

        /// <summary>
        /// Adds a new category or updates an existing one.
        /// </summary>
        /// <param name="model">The category data to add or update.</param>
        /// <returns>A status message indicating success or failure.</returns>
        [HttpPost]
        public IActionResult AddUpdate(Category model)
        {
            var status = new StatusDto();

            // Check if the incoming model is valid based on its annotations
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0; // 0 indicates failure
                status.Message = "Validation failed";
                return Ok(status); // Return early if validation fails
            }

            // Perform add or update operation in the repository
            var result = _categoryRepository.AddUpdate(model);

            // Set the status based on the result of the operation
            status.StatusCode = result ? 1 : 0; // 1 for success, 0 for failure
            status.Message = result ? "Saved successfully" : "Error has occurred";
            return Ok(status); // Return HTTP 200 with the status message
        }

        /// <summary>
        /// Deletes a category by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the category to delete.</param>
        /// <returns>A status message indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Perform the delete operation using the repository
            var result = _categoryRepository.Delete(id);

            // Create a status message based on the result of the delete operation
            var status = new StatusDto
            {
                StatusCode = result ? 1 : 0, // 1 for success, 0 for failure
                Message = result ? "Deleted successfully" : "Error has occurred"
            };

            return Ok(status); // Return HTTP 200 with the status message
        }
    }
}
