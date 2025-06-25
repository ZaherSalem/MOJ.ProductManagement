using Moq;
using AutoMapper;
using MOJ.ProductManagement.Domain.Interfaces;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Application.Features.Products.Commands.Add;
using MOJ.ProductManagement.Application.Features.Products.Commands.Edit;
using MOJ.ProductManagement.Application.Features.Products.Commands.Delete;

public class ProductCommandsTest
{
    [Fact]
    public async Task CreateProductCommandHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var dto = new CreateProductDto { Name = "Test Product", quantityPerUnitId = 0, ReorderLevel = 1, SupplierId = Guid.NewGuid(), UnitPrice = 10, UnitsInStock = 5, UnitsOnOrder = 2 };
        var product = new Product();
        var productDto = new ProductDto { Id = 1, Name = dto.Name };

        mapper.Setup(m => m.Map<Product>(dto)).Returns(product);
        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.AddAsync(product)).Returns(Task.FromResult(product));
        mapper.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

        var handler = new CreateProductCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new CreateProductCommand(dto);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.Equal(productDto.Name, result.Data.Name);
    }

    [Fact]
    public async Task UpdateProductCommandHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var id = 1;
        var product = new Product();
        var dto = new UpdateProductDto { Id = id, Name = "Updated Product", quantityPerUnitId = 0, ReorderLevel = 1, SupplierId = 1, UnitPrice = 10, UnitsInStock = 5, UnitsOnOrder = 2 };
        var updatedProduct = new Product();
        var productDto = new ProductDto { Id = id, Name = dto.Name };

        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
        mapper.Setup(m => m.Map(dto, product)).Returns(updatedProduct);
        repo.Setup(r => r.UpdateAsync(updatedProduct)).Returns(Task.CompletedTask);
        mapper.Setup(m => m.Map<ProductDto>(updatedProduct)).Returns(productDto);

        var handler = new UpdateProductCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new UpdateProductCommand(dto);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.Equal(dto.Name, result.Data.Name);
    }

    [Fact]
    public async Task UpdateProductCommandHandler_ShouldReturnFailure_WhenNotFound()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var id = 1;
        var dto = new UpdateProductDto { Id = id, Name = "Updated Product", quantityPerUnitId = 0, ReorderLevel = 1, SupplierId = 1, UnitPrice = 10, UnitsInStock = 5, UnitsOnOrder = 2 };

        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product)null);

        var handler = new UpdateProductCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new UpdateProductCommand(dto);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("Product not found", result.Messages[0]);
    }

    [Fact]
    public async Task DeleteProductCommandHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var id = 1;
        var product = new Product();

        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(product);
        repo.Setup(r => r.DeleteAsync(product)).Returns(Task.CompletedTask);

        var handler = new DeleteProductCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new DeleteProductCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task DeleteProductCommandHandler_ShouldReturnFailure_WhenNotFound()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Product>>();
        var id = 1;

        unitOfWork.Setup(u => u.GetRepository<Product>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product)null);

        var handler = new DeleteProductCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new DeleteProductCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("Product not found", result.Messages[0]);
    }
}
