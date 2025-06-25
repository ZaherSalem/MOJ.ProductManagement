using FluentValidation;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Commands
{
    public class DeleteSupplierCommandValidtor : AbstractValidator<DeleteSupplierCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierCommandValidtor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(id => id)
                .NotEmpty().WithMessage("Supplier ID is required");

            //check if supplier have products
            RuleFor(id => id)
                .MustAsync(async (request, cancellation) =>
                {
                    return !await _unitOfWork.GetRepository<Product>().AnyAsync(_ => _.SupplierId == request.id);
                })
                .WithMessage("Cannot delete supplier with existing products");
        }
    }
}