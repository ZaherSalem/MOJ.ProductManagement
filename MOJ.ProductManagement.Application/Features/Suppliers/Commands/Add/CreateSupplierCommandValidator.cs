using FluentValidation;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Commands
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateSupplierCommandValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(s => s.dto.Name)
                .NotEmpty().WithMessage("Supplier name is required")
                .MaximumLength(100).WithMessage("Supplier name cannot exceed 100 characters");

            RuleFor(s => s.dto.Name)
                .MustAsync(async (name, cancellation) =>
                {
                    return !await _unitOfWork.GetRepository<Supplier>().AnyAsync(_ => _.Name == name);
                })
                .WithMessage("Supplier name must be unique");
        }
    }
}
