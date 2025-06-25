using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Suppliers.Commands
{
    public record DeleteSupplierCommand(int id) : IRequest<Result<bool>>;

    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Result<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteSupplierCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var prodcut = await _unitOfWork.GetRepository<Supplier>().GetByIdAsync(request.id);
            if (prodcut == null)
            {
                return Result<bool>.Failure("Supplier not found", 404);
            }

            _unitOfWork.GetRepository<Supplier>().DeleteAsync(prodcut);

            return Result<bool>.Success(true, 200);
        }
    }
}
