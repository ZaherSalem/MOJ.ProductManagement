using FluentValidation;

namespace MOJ.ProductManagement.Application.Features.Products.Commands.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(id => id)
                .NotEmpty().WithMessage("Supplier ID is required");
        }
    }
}