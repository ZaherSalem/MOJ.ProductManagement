using FluentValidation;
using MOJ.ProductManagement.Application.Features.Suppliers.Queries.Get;

namespace MOJ.ProductManagement.Application.Features.Products.Queries.Get
{
    public class GetSupplierQueryValidator : AbstractValidator<GetSupplierQuery>
    {
        public GetSupplierQueryValidator()
        {
            RuleFor(x => x.id)
             .NotEmpty().WithMessage("Invalid supplier ID");
        }
    }
}
