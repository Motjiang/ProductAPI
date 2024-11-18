using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models.Domain; // Importing domain models for the Product entity
using ProductAPI.Models.DTOs; // Importing Data Transfer Objects (DTOs) for communication
using ProductAPI.Repositories.Abstract; // Importing repository interface for product-related operations

namespace ProductAPI.Controllers
{
    // Define the route for this controller as "api/products"
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        // Constructor injection for the repository interface, which abstracts the data access logic
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Retrieves all products, optionally filtering them by a search term.
        /// </summary>
        /// <param name="term">Optional search term for filtering products.</param>
        /// <returns>A list of products matching the search criteria.</returns>
        [HttpGet]
        public IActionResult GetAll(string term = "")
        {
            // Fetches all products or filtered products from the repository
            var data = _productRepository.GetAll(term);
            return Ok(data); // Returns HTTP 200 with the product data
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product data or an appropriate response.</returns>
        [HttpGet("{id}")] // The route for this method is "api/products/{id}"
        public IActionResult GetById(int id)
        {
            // Fetch the product by ID
            var data = _productRepository.GetById(id);
            return Ok(data); // Returns HTTP 200 with the product data
        }

        /// <summary>
        /// Adds a new product or updates an existing one.
        /// </summary>
        /// <param name="model">The product data to add or update.</param>
        /// <returns>A status message indicating success or failure.</returns>
        [HttpPost]
        public IActionResult AddUpdate(Product model)
        {
            var status = new StatusDto();

            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0; // Indicates failure
                status.Message = "Validation failed";
                return Ok(status); // Return early if validation fails
            }

            // Add or update the product in the repository
            var result = _productRepository.AddUpdate(model);

            // Set the response status based on the operation result
            status.StatusCode = result ? 1 : 0; // 1 for success, 0 for failure
            status.Message = result ? "Saved successfully" : "Error has occurred";
            return Ok(status); // Return HTTP 200 with the status
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>A status message indicating success or failure.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Deletes the product from the repository
            var result = _productRepository.Delete(id);

            // Create a status message based on the operation result
            var status = new StatusDto
            {
                StatusCode = result ? 1 : 0, // 1 for success, 0 for failure
                Message = result ? "Deleted successfully" : "Error has occurred"
            };

            return Ok(status); // Return HTTP 200 with the status
        }
    }
}
