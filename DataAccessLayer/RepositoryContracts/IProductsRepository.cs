using System.Linq.Expressions;
using ECommerce.DataAccessLayer.Entites;

namespace ECommerce.DataAccessLayer.RepositoryContracts;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product,bool>>conditonExpression);
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditonExpression);
    Task<Product?> AddProduct(Product product);
    Task<Product?> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid productId);
}
