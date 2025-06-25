using FluentValidation;

namespace MOJ.ProductManagement.Application.Features.Products.Queries.Get
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.id)
             .NotEmpty().WithMessage("Invalid product ID");
        }
    }
}
