using ProductAPI.Data;
using ProductAPI.Models.Domain;
using ProductAPI.Repositories.Abstract;

namespace ProductAPI.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddUpdate(Category category)
        {
            try
            {
                if (category.Id == 0)
                {
                    _context.Category.Add(category);
                }
                else
                {
                    _context.Category.Update(category);                   
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
                _context.Category.Remove(record);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        public Category GetById(int id)
        {
            return _context.Category.Find(id);
        }
    }
}
