using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Domain.Aggregates;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Queries.Get
{
    public record GetSupplierQuery(int id) : IRequest<Result<SupplierDto>>;

    public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, Result<SupplierDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetSupplierQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SupplierDto>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            var prodcut = await _unitOfWork.GetRepository<Supplier>()
                .GetQueryable()
                .Where(_ => _.Id == request.id)
                .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (prodcut == null)
            {
                return Result<SupplierDto>.Failure("Supplier not found", 404);
            }


            return Result<SupplierDto>.Success(prodcut, 200);
        }
    }
}
