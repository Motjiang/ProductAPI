using ProductAPI.Data;
using ProductAPI.Models.Domain;
using ProductAPI.Repositories.Abstract;

namespace ProductAPI.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add or Update a product in the database
        public bool AddUpdate(Product product)
        {
            try
            {
                // Check if the product is new (Id = 0) or existing
                if (product.Id == 0)
                {
                    // Add new product to the database context
                    _context.Product.Add(product);
                }
                else
                {
                    // Update existing product in the database context
                    _context.Product.Update(product);
                }

                // Save changes to the database
                _context.SaveChanges();
                return true; // Indicate success
            }
            catch (System.Exception)
            {
                return false; // Return false if an exception occurs
            }
        }

        // Delete a product from the database by its ID
        public bool Delete(int id)
        {
            try
            {
                // Fetch the product to delete
                var record = GetById(id);
                if (record == null)
                {
                    // Return false if the product doesn't exist
                    return false;
                }

                // Remove the product from the database context
                _context.Product.Remove(record);

                // Save changes to the database
                _context.SaveChanges();
                return true; // Indicate success
            }
            catch (Exception ex)
            {
                return false; // Return false if an exception occurs
            }
        }

        // Retrieve all products from the database, optionally filtered by a search term
        public IEnumerable<Product> GetAll(string term = "")
        {
            // Convert the search term to lowercase for case-insensitive comparison
            term = term.ToLower();

            // Query to fetch products and their associated category names
            var data = (from product in _context.Product
                        join category in _context.Category
                        on product.CategoryId equals category.Id
                        where term == "" || product.Name.ToLower().StartsWith(term) // Filter by term if provided
                        select new Product
                        {
                            Id = product.Id,
                            CategoryId = product.CategoryId,
                            Name = product.Name,
                            CategoryName = category.Name, // Include category name
                            Price = product.Price
                        }).ToList(); // Execute the query and convert to a list

            return data; // Return the resulting list of products
        }

        // Retrieve a product by its ID
        public Product GetById(int id)
        {
            // Use Find to locate the product by its primary key
            return _context.Product.Find(id);
        }
    }
}
