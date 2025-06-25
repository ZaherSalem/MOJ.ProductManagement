using MediatR;
using Microsoft.AspNetCore.Mvc;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Application.Features.Products.Commands.Add;
using MOJ.ProductManagement.Application.Features.Products.Commands.Edit;
using MOJ.ProductManagement.Application.Features.Products.Queries.Get;
using MOJ.ProductManagement.Application.Features.Products.Queries.GetProducts;
using MOJ.ProductManagement.WebApi.Controllers;
using Moq;
using static MOJ.ProductManagement.WebApi.Enums.LookupEnums;

namespace MOJ.ProductManagement.Test.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateProduct_ReturnsOk_WhenSucceeded()
        {
            var dto = new CreateProductDto { Name = "Test", QuantityPerUnitId = (int)QuantityPerUnit.Liter, ReorderLevel = 1, SupplierId = 1, UnitPrice = 10, UnitsInStock = 5, UnitsOnOrder = 2 };
            var result = Result<ProductDto>.Success(new ProductDto { Id = 1, Name = "Test" });
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.CreateProduct(dto);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task CreateProduct_ReturnsBadRequest_WhenFailed()
        {
            var dto = new CreateProductDto { Name = "Test", QuantityPerUnitId = (int)QuantityPerUnit.Kilo, ReorderLevel = 1, SupplierId = 1, UnitPrice = 10, UnitsInStock = 5, UnitsOnOrder = 2 };
            var result = Result<ProductDto>.Failure("Error");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.CreateProduct(dto);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOk_WhenSucceeded()
        {
            var dto = new UpdateProductDto { Id = 1, Name = "Test", QuantityPerUnitId = (int)QuantityPerUnit.Kilo, ReorderLevel = 1, SupplierId = 1, UnitPrice = 10, UnitsInStock = 5, UnitsOnOrder = 2 };
            var result = Result<ProductDto>.Success(new ProductDto { Id = dto.Id, Name = "Test" });
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.UpdateProduct(dto.Id, dto);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_WhenFailed()
        {
            // Arrange
            var dto = new UpdateProductDto
            {
                Id = 1,
                Name = "Test",
                QuantityPerUnitId = (int)QuantityPerUnit.Liter,
                ReorderLevel = 1,
                SupplierId = 1,
                UnitPrice = 10,
                UnitsInStock = 5,
                UnitsOnOrder = 2
            };

            var errorMessage = "Update failed";
            var failedResult = Result<ProductDto>.Failure(errorMessage);

            _mediatorMock.Setup(m => m.Send(
                It.Is<UpdateProductCommand>(cmd =>
                    cmd.dto.Id == dto.Id &&
                    cmd.dto == dto),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(failedResult);

            // Act
            var response = await _controller.UpdateProduct(dto.Id, dto);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Result<ProductDto>>>(response);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            var result = Assert.IsType<Result<ProductDto>>(badRequestResult.Value);

            Assert.False(result.Succeeded);
            Assert.Contains(errorMessage, result.Messages);
        }

        [Fact]
        public async Task GetProduct_ReturnsOk_WhenSucceeded()
        {
            var id = 1;
            var result = Result<ProductDto>.Success(new ProductDto { Id = id, Name = "Test" });
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.GetProduct(id);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetProduct_ReturnsBadRequest_WhenFailed()
        {
            var id = 1;
            var result = Result<ProductDto>.Failure("Error");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var response = await _controller.GetProduct(id);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnOkObjectResultWithProducts_WhenQuerySucceeds()
        {
            // Arrange
            var testProduct = new ProductDto
            {
                Id = 1,
                Name = "Test Product"
            };

            var paginatedResult = PaginatedResult<ProductDto>.Create(
                new List<ProductDto> { testProduct },
                1, // currentPage
                10, // pageSize
                1 // totalCount
            );

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(paginatedResult);

            // Act
            var paginatedRequest = new PaginatedRequest { PageNumber = 1, PageSize = 10 };
            var response = await _controller.GetProducts(paginatedRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnedResult = Assert.IsType<PaginatedResult<ProductDto>>(okResult.Value);
            Assert.Single(returnedResult.Data);
            Assert.Equal(testProduct.Id, returnedResult.Data[0].Id);
            Assert.Equal(testProduct.Name, returnedResult.Data[0].Name);
        }
    }
}
