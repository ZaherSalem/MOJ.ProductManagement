using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Products.Commands.Edit
{
    public record UpdateProductCommand(UpdateProductDto dto) : IRequest<Result<ProductDto>>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var prodcut = await _unitOfWork.GetRepository<Product>().GetByIdAsync(request.dto.Id);
            if (prodcut == null)
            {
                return Result<ProductDto>.Failure("Product not found", 404);
            }

            prodcut = _mapper.Map(request.dto, prodcut);

            await _unitOfWork.GetRepository<Product>().UpdateAsync(prodcut);
            var result = _mapper.Map<ProductDto>(prodcut);

            return Result<ProductDto>.Success(result, "200");
        }
    }
}
