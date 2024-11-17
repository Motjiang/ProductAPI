using ProductAPI.Data;
using ProductAPI.Models.Domain;
using ProductAPI.Repositories.Abstract;

namespace ProductAPI.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddUpdate(Product product)
        {
            try
            {
                if (product.Id == 0)
                {
                    _context.Product.Add(product);
                }
                else
                {
                    _context.Product.Update(product);
                }                  
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var record = GetById(id);
                if (record == null)
                {
                    return false;
                }
                _context.Product.Remove(record);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Product> GetAll(string term = "")
        {
            term = term.ToLower();
            var data = (from product in _context.Product
                        join
                        category in _context.Category
                        on product.CategoryId equals category.Id
                        where term == "" || product.Name.ToLower().StartsWith(term)
                        select new Product
                        {
                            Id = product.Id,
                            CategoryId = product.CategoryId,
                            Name = product.Name,
                            CategoryName = category.Name,
                            Price = product.Price
                        }
                        ).ToList();
            return data;
        }


        public Product GetById(int id)
        {
            return _context.Product.Find(id);
        }
    }
}
