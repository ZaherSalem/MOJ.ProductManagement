using FluentValidation;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Commands.Edit
{
    public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
    {
        public UpdateSupplierCommandValidator()
        {
            RuleFor(id => id)
               .NotEmpty().WithMessage("Supplier ID is required");

            RuleFor(s => s.dto.Name)
                .NotEmpty().WithMessage("Supplier name is required")
                .MaximumLength(100).WithMessage("Supplier name cannot exceed 100 characters");
        }
    }
}