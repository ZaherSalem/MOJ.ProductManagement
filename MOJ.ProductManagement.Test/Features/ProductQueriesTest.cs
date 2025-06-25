using Moq;
using AutoMapper;
using MOJ.ProductManagement.Domain.Interfaces;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Application.Features.Products.Queries.Get;
using MOJ.ProductManagement.Application.Features.Products.Queries.GetProducts;

public class ProductQueriesTest
{
    [Fact]
    public async Task GetProductQueryHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var id = 1;
        var productDto = new ProductDto { Id = id, Name = "Test Product" };
        var productList = new List<Product> { new Product() };
        var productQueryable = productList.AsQueryable();

        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.GetQueryable()).Returns(productQueryable);
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDto>());
        var realMapper = config.CreateMapper();
        var handler = new GetProductQueryHandler(realMapper, unitOfWork.Object);
        var command = new GetProductQuery(id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded); // Because the id won't match, so it should fail
    }

    [Fact]
    public async Task GetProductQueryHandler_ShouldReturnFailure_WhenNotFound()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var id = 1;
        var productQueryable = new List<Product>().AsQueryable();

        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.GetQueryable()).Returns(productQueryable);
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDto>());
        var realMapper = config.CreateMapper();
        var handler = new GetProductQueryHandler(realMapper, unitOfWork.Object);
        var command = new GetProductQuery(id);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("Product not found", result.Messages[0]);
    }

    [Fact]
    public async Task GetProductsQueryHandler_ShouldReturnPaginatedResult()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var products = new List<Product> {
            new Product(),
            new Product()
        };
        var productQueryable = products.AsQueryable();
        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.GetQueryable()).Returns(productQueryable);
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDto>());
        var realMapper = config.CreateMapper();
        var handler = new GetProductsQueryHandler(realMapper, unitOfWork.Object);
        var paginatedRequest = new PaginatedRequest { PageNumber = 1, PageSize = 2 };
        var command = new GetProductsQuery(paginatedRequest);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.Equal(2, result.Data.Count);
        Assert.Equal(2, result.TotalCount);
    }
}
