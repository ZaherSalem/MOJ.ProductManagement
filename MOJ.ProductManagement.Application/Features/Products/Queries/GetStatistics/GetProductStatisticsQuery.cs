using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Domain.Aggregates;
using AutoMapper.QueryableExtensions;
using MOJ.ProductManagement.Application.DTOs;
using MOJ.ProductManagement.Application.DTOs.Common;

namespace MOJ.ProductManagement.Application.Features.Products.Queries.GetProducts
{
    public record GetProductStatisticsQuery() : IRequest<Result<ProductStatisticsDto>>;

    public class GetProductStatisticsQueryHandler : IRequestHandler<GetProductStatisticsQuery, Result<ProductStatisticsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetProductStatisticsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductStatisticsDto>> Handle(GetProductStatisticsQuery request, CancellationToken cancellationToken)
        {
            var productRepo = _unitOfWork.GetRepository<Product>();
            var supplierRepo = _unitOfWork.GetRepository<Supplier>();

            // 1. Products to reorder (adjust property names as needed)
            var productsToReorder = await productRepo.GetQueryable()
                                                     .Where(p => p.UnitsInStock <= p.ReorderLevel)
                                                     .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                                     .ToListAsync(cancellationToken);


            // 2. Largest supplier (by number of products)
            var largestSupplier = await productRepo.GetQueryable()
                                                     .GroupBy(p => p.Supplier)
                                                     .OrderByDescending(g => g.Count())
                                                     .Select(g => g.Key)
                                                     .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                                                     .Cast<SupplierDto?>() // Ensure nullable for FirstOrDefaultAsync
                                                     .FirstOrDefaultAsync(cancellationToken);


            // 3. Product with minimum orders (adjust if you have a different order structure)
            // Since there is no Order table, find the product with the minimum quantity (or another relevant property)
            var productWithMinOrders = await productRepo.GetQueryable()
                                                        .OrderBy(p => p.UnitsOnOrder)
                                                        .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                                        .FirstOrDefaultAsync(cancellationToken);
            var result = new ProductStatisticsDto
            {
                ProductsToReorder = productsToReorder,
                LargestSupplier = largestSupplier,
                ProductWithMinimumOrders = productWithMinOrders
            };

            return Result<ProductStatisticsDto>.Success(result, 200);
        }
    }
}
