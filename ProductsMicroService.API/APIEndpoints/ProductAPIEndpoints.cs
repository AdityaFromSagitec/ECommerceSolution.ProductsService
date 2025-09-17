using System; 
using System.Collections.Generic;
using BussinessLogicLayer.DTO;
using DataAccessLayer.Entites;
using ECommerce.BussinessLogicLayer.ServiceContracts;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.ProductsMicroService.API.APIEndpoints;

public static class ProductAPIEndpoints
{
    public static IEndpointRouteBuilder MapProductAPIEndpoints(this IEndpointRouteBuilder app)
    {
        //GET /api/products
        app.MapGet("api/products", async (IProductService productService) =>
        {
            List<ProductResponse?> products = await productService.GetProducts();
            return Results.Ok(products);
        });

        //GET /api/products/search/product-id/00000000-0000-0000-0000-000000000000 // the last parameter is guid
        app.MapGet("/api/products/search/product-id/{ProductID:guid}", async (IProductService productService, Guid ProductID) =>
        {
            ProductResponse? products = await productService.GetProductByCondition(temp => temp.ProductID == ProductID);
            return Results.Ok(products);
        });
        //GET /api/products/search/product-id/xxxxxxxxxxxxxxxxxxxx // the last parameter is guid
        app.MapGet("/api/products/search/{SearchString}", async (IProductService productService, string SearchString) =>
        {
            List<ProductResponse?> productsByProductName = await productService.GetProductsByCondition(temp => temp.ProductName != null &&
            temp.ProductName.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            List<ProductResponse?> productsByCategory = await productService.GetProductsByCondition(temp => temp.Category != null &&
           temp.Category.Contains(SearchString, StringComparison.OrdinalIgnoreCase));

            var proudcts = productsByProductName.Union(productsByCategory);
            return Results.Ok(proudcts);
        });

        //Post /api/products
        app.MapPost("/api/products/",
                async (IProductService productService, IValidator<ProductAddRequest> productAddRequestValidator,
                    ProductAddRequest productAddRequest) =>
        {
            ValidationResult validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key,
                    grp => grp.Select(err => err.ErrorMessage).ToArray());
                return Results.ValidationProblem(errors);
            }

            ProductResponse? addedProductResponse = await productService.AddProduct(productAddRequest);
            if (productAddRequest != null)
            {
                return Results.Created($"/api/products/search/product-id/{addedProductResponse.ProductID}", addedProductResponse);

            }
            else
            {
                return Results.Problem("Error in adding product");
            }
        });


        //Put /api/products
        app.MapPut("/api/products/",
                async (IProductService productService, IValidator<ProductUpdateRequest> productUpdateRequestValidator,
                    ProductUpdateRequest productUpdateRequest) =>
                {
                    ValidationResult validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);
                    if (!validationResult.IsValid)
                    {
                        Dictionary<string, string[]> errors = validationResult.Errors.GroupBy(temp => temp.PropertyName).ToDictionary(grp => grp.Key,
                            grp => grp.Select(err => err.ErrorMessage).ToArray());
                        return Results.ValidationProblem(errors);
                    }

                    ProductResponse? updatedProductResponse = await productService.UpdateProduct(productUpdateRequest);
                   
                    return updatedProductResponse != null ? Results.Ok(updatedProductResponse) 
                                        : Results.Problem("Error in updating product");
                });

        //Delete /api/products/product-id/00000000-0000-0000-0000-000000000000
        app.MapDelete("/api/products/{ProductID:guid}",
                async (IProductService productService,Guid ProductID ) =>
                {
                    bool IsDeleted = await productService.DeleteProduct(ProductID);
                    if (IsDeleted)
                    {
                        return Results.Ok($"Product with id {ProductID} deleted successfully");
                    }
                    else
                    {
                        return Results.NotFound($"Product with id {ProductID} not found");
                    }
                });


        return app;
    }
}
