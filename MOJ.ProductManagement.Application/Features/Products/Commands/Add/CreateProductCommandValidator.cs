using FluentValidation;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Products.Commands.Add
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.dto.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters");

            RuleFor(x => x.dto.Name)
                .MustAsync(async (name, cancellation) =>
                {
                    return !await _unitOfWork.GetRepository<Product>().AnyAsync(_ => _.Name == name);
                })
                .WithMessage("Product name must be unique");

            RuleFor(x => x.dto.QuantityPerUnitId)
                .GreaterThanOrEqualTo(10).WithMessage("Invalid quantity unit specified");

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


        }
    }
}
