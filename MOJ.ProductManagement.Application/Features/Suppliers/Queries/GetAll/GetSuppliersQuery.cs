using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Application.Extensions;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Queries.GetAll
{
    public record GetSuppliersQuery(PaginatedRequest dto) : IRequest<PaginatedResult<SupplierDto>>;

    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, PaginatedResult<SupplierDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetSuppliersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<PaginatedResult<SupplierDto>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _unitOfWork.GetRepository<Supplier>()
                                            .GetQueryable()
                                            .ToPaginatedListAsync<Supplier, SupplierDto>(_mapper, request.dto, cancellationToken);

            return suppliers;
        }
    }
}
