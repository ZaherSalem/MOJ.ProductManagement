using Moq;
using AutoMapper;
using MOJ.ProductManagement.Domain.Interfaces;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Application.Features.Suppliers.Commands;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Application.Features.Suppliers.Commands.Edit;

public class SupplierFuturesTest
{
    [Fact]
    public async Task CreateSupplierCommandHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var dto = new CreateSupplierDto { Name = "Test Supplier" };
        var supplier = new Supplier { Name = dto.Name };
        var supplierDto = new SupplierDto { Id = 1, Name = dto.Name };

        mapper.Setup(m => m.Map<Supplier>(dto)).Returns(supplier);
        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.AddAsync(supplier)).Returns(Task.FromResult(supplier));
        mapper.Setup(m => m.Map<SupplierDto>(supplier)).Returns(supplierDto);

        var handler = new CreateSupplierCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new CreateSupplierCommand(dto);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.Equal(supplierDto.Name, result.Data.Name);
    }

    [Fact]
    public async Task UpdateSupplierCommandHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var id = 1;
        var supplier = new Supplier { Name = "Old Name" };
        var dto = new SupplierDto { Id = id, Name = "New Name" };
        var updatedSupplier = new Supplier { Name = dto.Name };

        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(supplier);
        mapper.Setup(m => m.Map(dto, supplier)).Returns(updatedSupplier);
        repo.Setup(r => r.UpdateAsync(updatedSupplier)).Returns(Task.CompletedTask);
        mapper.Setup(m => m.Map<SupplierDto>(updatedSupplier)).Returns(dto);

        var handler = new UpdateSupplierCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new UpdateSupplierCommand(id, dto);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.Equal(dto.Name, result.Data.Name);
    }

    [Fact]
    public async Task UpdateSupplierCommandHandler_ShouldReturnFailure_WhenNotFound()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var id = 1;
        var dto = new SupplierDto { Id = id, Name = "New Name" };

        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Supplier)null);

        var handler = new UpdateSupplierCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new UpdateSupplierCommand(id, dto);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("Product not found", result.Messages[0]);
    }

    [Fact]
    public async Task DeleteSupplierCommandHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var id = 1;
        var supplier = new Supplier { Name = "ToDelete" };

        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(supplier);
        repo.Setup(r => r.DeleteAsync(supplier)).Returns(Task.CompletedTask);

        var handler = new DeleteSupplierCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new DeleteSupplierCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task DeleteSupplierCommandHandler_ShouldReturnFailure_WhenNotFound()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var id = 1;

        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Supplier)null);

        var handler = new DeleteSupplierCommandHandler(mapper.Object, unitOfWork.Object);
        var command = new DeleteSupplierCommand(id);
        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("Supplier not found", result.Messages[0]);
    }
}
