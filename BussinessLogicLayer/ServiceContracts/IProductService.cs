using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BussinessLogicLayer.DTO;
using DataAccessLayer.Entites;

namespace ECommerce.BussinessLogicLayer.ServiceContracts;
public interface IProductService
{
    Task<List<ProductResponse?>> GetProducts();
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditonExpression);
    Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditonExpression);
    Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest);
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest);
    Task<bool> DeleteProduct(Guid productId);

}
