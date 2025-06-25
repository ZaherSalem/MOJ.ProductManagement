using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Products.Commands
{
    public record CreateProductCommand(CreateProductDto dto) : IRequest<Result<ProductDto>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var prodcut = _mapper.Map<Product>(request.dto);

            await _unitOfWork.GetRepository<Product>().AddAsync(prodcut);

            var result = _mapper.Map<ProductDto>(prodcut);

            return Result<ProductDto>.Success(result, "200");
        }
    }
}
