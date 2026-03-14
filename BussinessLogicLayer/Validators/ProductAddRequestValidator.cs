using ECommerce.BussinessLogicLayer.DTO;
using FluentValidation;

namespace ECommerce.BussinessLogicLayer.Validators;

public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        RuleFor(product => product.ProductName)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");
        RuleFor(product => product.Category)
            .IsInEnum().WithMessage("Invalid category.");
        RuleFor(product => product.UnitPrice)
            .InclusiveBetween(0, double.MaxValue).When(product => product.UnitPrice.HasValue)
            .WithMessage("Unit price must be greater than or equal to 0.");
        RuleFor(product => product.QuantityInStock)
            .GreaterThanOrEqualTo(0).When(product => product.QuantityInStock.HasValue)
            .WithMessage("Quantity in stock must be greater than or equal to 0.");
    }
}
