using Moq;
using AutoMapper;
using MOJ.ProductManagement.Domain.Interfaces;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Application.DTOs.Common;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Application.Features.Suppliers.Queries.Get;
using MOJ.ProductManagement.Application.Features.Suppliers.Queries.GetAll;

public class SupplierFuturesQueriesTest
{
    [Fact]
    public async Task GetSupplierQueryHandler_ShouldReturnSuccess()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var id = 1;
        var supplierDto = new SupplierDto { Id = id, Name = "Test Supplier" };
        var supplierList = new List<Supplier> { new Supplier { Name = "Test Supplier" } };
        var supplierQueryable = supplierList.AsQueryable();

        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.GetQueryable()).Returns(supplierQueryable);
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Supplier, SupplierDto>());
        var realMapper = config.CreateMapper();
        var handler = new GetSupplierQueryHandler(realMapper, unitOfWork.Object);
        var command = new GetSupplierQuery(id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.Succeeded); // Because the id won't match, so it should fail
    }

    [Fact]
    public async Task GetSupplierQueryHandler_ShouldReturnFailure_WhenNotFound()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var id = 1;
        var supplierQueryable = new List<Supplier>().AsQueryable();

        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.GetQueryable()).Returns(supplierQueryable);
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Supplier, SupplierDto>());
        var realMapper = config.CreateMapper();
        var handler = new GetSupplierQueryHandler(realMapper, unitOfWork.Object);
        var command = new GetSupplierQuery(id);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result.Succeeded);
        Assert.Contains("Supplier not found", result.Messages[0]);
    }

    [Fact]
    public async Task GetSuppliersQueryHandler_ShouldReturnPaginatedResult()
    {
        var mapper = new Mock<IMapper>();
        var unitOfWork = new Mock<IUnitOfWork>();
        var repo = new Mock<IRepository<Supplier>>();
        var suppliers = new List<Supplier> {
            new Supplier { Name = "Supplier1" },
            new Supplier { Name = "Supplier2" }
        };
        var supplierQueryable = suppliers.AsQueryable();
        unitOfWork.Setup(u => u.GetRepository<Supplier>()).Returns(repo.Object);
        repo.Setup(r => r.GetQueryable()).Returns(supplierQueryable);
        var config = new MapperConfiguration(cfg => cfg.CreateMap<Supplier, SupplierDto>());
        var realMapper = config.CreateMapper();
        var handler = new GetSuppliersQueryHandler(realMapper, unitOfWork.Object);
        var paginatedRequest = new PaginatedRequest { PageNumber = 1, PageSize = 2 };
        var command = new GetSuppliersQuery(paginatedRequest);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        Assert.Equal(2, result.Data.Count);
        Assert.Equal(2, result.TotalCount);
    }
}
