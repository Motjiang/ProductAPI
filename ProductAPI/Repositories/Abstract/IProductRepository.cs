using ProductAPI.Models.Domain;

namespace ProductAPI.Repositories.Abstract
{
    public interface IProductRepository
    {
        bool AddUpdate(Product Product);
        bool Delete(int id);
        Product GetById(int id);
        IEnumerable<Product> GetAll(string term = "");
    }
}
