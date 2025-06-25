using AutoMapper;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Repositories;

namespace MOJ.ProductManagement.Application.Features.Products.Commands.Delete
{
    public record DeleteProductCommand(int id) : IRequest<Result<bool>>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var prodcut = await _unitOfWork.GetRepository<Product>().GetByIdAsync(request.id);
            if (prodcut == null)
            {
                return Result<bool>.Failure("Product not found", 404);
            }

            _unitOfWork.GetRepository<Product>().DeleteAsync(prodcut);

            return Result<bool>.Success(true, 200);
        }
    }
}
