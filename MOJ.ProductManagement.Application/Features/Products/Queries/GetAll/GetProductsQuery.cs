using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Application.Extensions;

namespace MOJ.ProductManagement.Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery(PaginatedRequest dto) : IRequest<PaginatedResult<ProductDto>>;

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetProductsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var prodcuts = await _unitOfWork.GetRepository<Product>()
                                            .GetQueryable()
                                            .Where(_=> request.dto.SearchValue == null
                                                    || (_.Name.Contains(request.dto.SearchValue)|| _.Supplier.Name.Contains(request.dto.SearchValue)) )
                                            .ToPaginatedListAsync<Product, ProductDto>(_mapper, request.dto, cancellationToken);

            return prodcuts;
        }
    }
}
