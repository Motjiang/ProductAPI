using ProductAPI.Data;
using ProductAPI.Models.Domain;
using ProductAPI.Repositories.Abstract;

namespace ProductAPI.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add or Update a category in the database
        public bool AddUpdate(Category category)
        {
            try
            {
                // Check if the category is new (Id = 0) or existing
                if (category.Id == 0)
                {
                    // Add new category to the database context
                    _context.Category.Add(category);
                }
                else
                {
                    // Update existing category in the database context
                    _context.Category.Update(category);
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

        // Delete a category from the database by its ID
        public bool Delete(int id)
        {
            try
            {
                // Fetch the category to delete
                var record = GetById(id);
                if (record == null)
                {
                    // Return false if the category doesn't exist
                    return false;
                }

                // Remove the category from the database context
                _context.Category.Remove(record);

                // Save changes to the database
                _context.SaveChanges();
                return true; // Indicate success
            }
            catch (System.Exception)
            {
                return false; // Return false if an exception occurs
            }
        }

        // Retrieve all categories from the database
        public IEnumerable<Category> GetAll()
        {
            // Fetch and return all categories as a list
            return _context.Category.ToList();
        }

        // Retrieve a category by its ID
        public Category GetById(int id)
        {
            // Use Find to locate the category by its primary key
            return _context.Category.Find(id);
        }
    }
}
