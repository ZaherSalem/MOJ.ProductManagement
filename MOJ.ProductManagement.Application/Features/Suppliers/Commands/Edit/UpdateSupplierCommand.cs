using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Commands.Edit
{
    public record UpdateSupplierCommand(int id, SupplierDto dto) : IRequest<Result<SupplierDto>>;

    public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Result<SupplierDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SupplierDto>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _unitOfWork.GetRepository<Supplier>().GetByIdAsync(request.id);
            if (supplier == null)
            {
                return Result<SupplierDto>.Failure("Product not found", 404);
            }

            supplier = _mapper.Map(request.dto, supplier);

            await _unitOfWork.GetRepository<Supplier>().UpdateAsync(supplier);
            var result = _mapper.Map<SupplierDto>(supplier);

            return Result<SupplierDto>.Success(result, "200");
        }
    }
}
