
using System.Linq.Expressions;
using AutoMapper;
using ECommerce.BussinessLogicLayer.DTO;
using ECommerce.DataAccessLayer.Entites;
using ECommerce.BussinessLogicLayer.ServiceContracts;
using ECommerce.DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.BussinessLogicLayer.Services;

public class ProductService : IProductService
{
    private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
    private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public ProductService(IValidator<ProductAddRequest> productAddRequestValidator, IValidator<ProductUpdateRequest> productUpdateRequestValidator, 
                          IMapper mapper, IProductsRepository productsRepository)
    {
        _productAddRequestValidator = productAddRequestValidator;
        _productUpdateRequestValidator = productUpdateRequestValidator;
        _mapper = mapper;
        _productsRepository = productsRepository;
    }
    public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
    {
        if (productAddRequest == null)
        {
            throw new ArgumentNullException(nameof(productAddRequest));
        }
        ValidationResult validationResult = await _productAddRequestValidator.ValidateAsync(productAddRequest);
        if (!validationResult.IsValid)
        {
            string errors = string.Join(", " ,validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }
        //Map from ProductAddRequest to Product
        Product product = _mapper.Map<Product>(productAddRequest);
        Product? addedProduct = await _productsRepository.AddProduct(product);
        if (addedProduct == null)
        {
            return null;
        }
        //Map form Product to ProductResponse
        return _mapper.Map<ProductResponse>(addedProduct);
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        Product? existingProduct = _productsRepository.GetProductByCondition(temp => temp.ProductID == productId).Result;
        if (existingProduct == null)
        {
            return false;
        }
         bool IsDeleted = await _productsRepository.DeleteProduct(productId);
        return IsDeleted;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditonExpression)
    {
        Product? product = await _productsRepository.GetProductByCondition(conditonExpression);
        if (product == null)
        {
            return null;
        }
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
       IEnumerable<Product?> products = await _productsRepository.GetProducts();
        IEnumerable<ProductResponse?> productResponses = _mapper.Map<IEnumerable<ProductResponse?>>(products);
        return productResponses.ToList();
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditonExpression)
    {
        //mapper is syncronous so we need to apply await on the methos only on the repository method
        return _mapper.Map<IEnumerable<ProductResponse?>>(await _productsRepository.GetProductsByCondition(conditonExpression)).ToList();
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
    {
        Product? existingProduct = _productsRepository.GetProductByCondition (temp => temp.ProductID == productUpdateRequest.ProductID).Result;
        if (existingProduct == null)
        {
            return null;
        }
        ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
        if (!validationResult.IsValid)
        {
                       string errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ArgumentException(errors);
        }
        //Map from ProductUpdateRequest to Product
        Product product = _mapper.Map<Product>(productUpdateRequest);
        Product? updatedProduct = await _productsRepository.UpdateProduct(product);
        if (updatedProduct == null)
        {
            return null;
        }
        //Map form Product to ProductResponse
        return _mapper.Map<ProductResponse>(updatedProduct);
    }
}
