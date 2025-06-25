using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.Features.Suppliers.Commands;
using MOJ.ProductManagement.WebApi.Controllers;
using MOJ.ProductManagement.Application.Features.Suppliers.Commands.Edit;
using MOJ.ProductManagement.Application.Features.Suppliers.Queries.Get;
using MOJ.ProductManagement.Application.Features.Suppliers.Queries.GetAll;

namespace MOJ.ProductManagement.Test.Controllers
{
    public class SuppliersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly SuppliersController _controller;

        public SuppliersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new SuppliersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateSupplier_ReturnsOk_WhenSucceeded()
        {
            var dto = new CreateSupplierDto { Name = "Test Supplier" };
            var result = Result<SupplierDto>.Success(new SupplierDto { Id = 1, Name = "Test Supplier" });
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateSupplierCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.CreateSupplier(dto);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task CreateSupplier_ReturnsBadRequest_WhenFailed()
        {
            var dto = new CreateSupplierDto { Name = "Test Supplier" };
            var result = Result<SupplierDto>.Failure("Error");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateSupplierCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.CreateSupplier(dto);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task UpdateSupplier_ReturnsOk_WhenSucceeded()
        {
            var id = 1;
            var dto = new SupplierDto { Id = id, Name = "Test Supplier" };
            var result = Result<SupplierDto>.Success(dto);
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateSupplierCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.UpdateSupplier(id, dto);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task UpdateSupplier_ReturnsBadRequest_WhenFailed()
        {
            var id = 1;
            var dto = new SupplierDto { Id = id, Name = "Test Supplier" };
            var result = Result<SupplierDto>.Failure("Error");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateSupplierCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.UpdateSupplier(id, dto);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetSupplier_ReturnsOk_WhenSucceeded()
        {
            var id = 1;
            var result = Result<SupplierDto>.Success(new SupplierDto { Id = id, Name = "Test Supplier" });
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetSupplierQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.GetSupplier(id);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetSupplier_ReturnsBadRequest_WhenFailed()
        {
            var id = 1;
            var result = Result<SupplierDto>.Failure("Error");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetSupplierQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.GetSupplier(id);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetSuppliers_ReturnsOk_WhenSucceeded()
        {
            // Arrange
            var testSupplier = new SupplierDto
            {
                Id = 1,
                Name = "Test Product"
            };

            var paginatedResult = PaginatedResult<SupplierDto>.Create(
                new List<SupplierDto> { testSupplier },
                1, // currentPage
                10, // pageSize
                1 // totalCount
            );

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetSuppliersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paginatedResult);

            // Act
            var response = await _controller.GetSuppliers(new PaginatedRequest());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnedResult = Assert.IsType<PaginatedResult<SupplierDto>>(okResult.Value);
            Assert.Single(returnedResult.Data);
            Assert.Equal(testSupplier.Id, returnedResult.Data[0].Id);
            Assert.Equal(testSupplier.Name, returnedResult.Data[0].Name);
        }
    }
}
