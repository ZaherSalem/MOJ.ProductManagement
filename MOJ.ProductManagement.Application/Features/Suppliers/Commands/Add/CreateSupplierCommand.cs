using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Commands
{
    public record CreateSupplierCommand(CreateSupplierDto dto) : IRequest<Result<SupplierDto>>;

    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Result<SupplierDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {

            var prodcut = _mapper.Map<Supplier>(request.dto);

            await _unitOfWork.GetRepository<Supplier>().AddAsync(prodcut);

            var result = _mapper.Map<SupplierDto>(prodcut);

            return Result<SupplierDto>.Success(result, "200");
        }
    }
}
