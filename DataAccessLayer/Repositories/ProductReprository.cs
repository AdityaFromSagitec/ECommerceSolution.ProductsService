using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerce.DataAccessLayer.Entites;
using ECommerce.DataAccessLayer.RepositoryContracts;
using ECommerce.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ECommerce.DataAccessLayer.Repositories;

public class ProductReprository : IProductsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ProductReprository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }   
    public async Task<Product?> AddProduct(Product product)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
     
        Product? existingProduct = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductID == productId);

        if (existingProduct == null)
        {
            return false;
        }
        _dbContext.Products.Remove(existingProduct);
        int affectedRowscount = await _dbContext.SaveChangesAsync();
        return affectedRowscount > 0;
    }
    public async Task<Product?> UpdateProduct(Product product)
    {
        if (_dbContext.Products.Any(p => p.ProductID == product.ProductID))
        {
            await _dbContext.SaveChangesAsync();
            return product;
        }
        return null;
    }
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _dbContext.Products.ToListAsync();
    }
    public async Task<IEnumerable<Product?>> GetProductsByCondition(Expression<Func<Product, bool>> conditonExpression)
    {
        return await _dbContext.Products.Where(conditonExpression).ToListAsync();
    }
    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> conditonExpression)
    {
         return await _dbContext.Products.FirstOrDefaultAsync(conditonExpression);
    }

   


}
