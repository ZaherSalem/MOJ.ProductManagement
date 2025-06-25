using FluentValidation;

namespace MOJ.ProductManagement.Application.Features.Products.Commands.Edit
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.dto.Id)
             .NotEmpty().WithMessage("Invalid product ID");

            RuleFor(x => x.dto.Name)
                 .NotEmpty().WithMessage("Product name is required")
                 .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

            RuleFor(x => x.dto.quantityPerUnitId)
                .IsInEnum().WithMessage("Invalid quantity unit specified");

            RuleFor(x => x.dto.ReorderLevel)
                .GreaterThanOrEqualTo(0).WithMessage("Reorder level must be zero or positive");

            RuleFor(x => x.dto.SupplierId)
                .NotEmpty().WithMessage("Supplier must be specified");

            RuleFor(x => x.dto.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be positive")
                .Custom((value, context) =>
                {
                    if (decimal.Round(value, 2) != value)
                    {
                        context.AddFailure("Unit price must have maximum 2 decimal places");
                    }
                });

            RuleFor(x => x.dto.UnitsInStock)
                .GreaterThanOrEqualTo(0).WithMessage("Units in stock cannot be negative");

            RuleFor(x => x.dto.UnitsOnOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Units on order cannot be negative");

           RuleFor(x => x)
               .Must(req => req.dto.UnitsInStock >= 0 && req.dto.UnitsOnOrder >= 0)
               .WithMessage("Stock and order values must be positive")
               .When(x => x.dto.UnitsInStock < x.dto.ReorderLevel)
               .WithMessage("Cannot update to stock level below reorder threshold without pending orders");
        }
    }
}
