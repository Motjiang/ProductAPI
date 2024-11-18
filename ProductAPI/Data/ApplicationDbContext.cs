using Microsoft.EntityFrameworkCore; // Importing EF Core for database operations
using ProductAPI.Models.Domain; // Importing domain models for Category and Product entities

namespace ProductAPI.Data
{
    // DbContext is the bridge between the application and the database
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor that accepts DbContextOptions for configuring the context.
        /// </summary>
        /// <param name="options">Options for configuring the database context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) // Passes the options to the base DbContext class
        {
        }

        /// <summary>
        /// Represents the Categories table in the database.
        /// </summary>
        public DbSet<Category> Category { get; set; }

        /// <summary>
        /// Represents the Products table in the database.
        /// </summary>
        public DbSet<Product> Product { get; set; }
    }
}
