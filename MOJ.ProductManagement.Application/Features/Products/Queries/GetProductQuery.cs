using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Application.DTOs.Product;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Products.Queries
{
    public record GetProductQuery(int id) : IRequest<Result<ProductDto>>;

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetProductQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var prodcut = await _unitOfWork.GetRepository<Product>()
                                           .GetQueryable()
                                           .Where(_ => _.Id == request.id)
                                           .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                           .SingleOrDefaultAsync(cancellationToken);

            if (prodcut == null)
            {
                return Result<ProductDto>.Failure("Product not found", 404);
            }


            return Result<ProductDto>.Success(prodcut, 200);
        }
    }
}
