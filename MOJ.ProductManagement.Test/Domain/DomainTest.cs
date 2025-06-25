using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Test.Domain
{
    public class DomainTest
    {
        [Fact]

        public void Supplier_CanHaveProducts()
        {
            // Arrange
            var supplier = new Supplier { Name = "Supplier1", Products = new System.Collections.Generic.List<Product>() };
            var product = Activator.CreateInstance(typeof(Product), true) as Product;
            Assert.NotNull(product);
            var nameProp = typeof(Product).GetProperty("Name");
            Assert.NotNull(nameProp);
            nameProp.SetValue(product, "Product1");
            supplier.Products.Add(product);

            // Assert
            Assert.Single(supplier.Products);
            Assert.Equal("Product1", supplier.Products.First().Name);
        }

       [Fact]
      public void Product_CanBeCreated_WithValidData()
      {
          // Arrange
          var supplier = new Supplier { Name = "Test Supplier" };
          var lookup = new Lookup { Name = "Box" };
          var product = Activator.CreateInstance(typeof(Product), true) as Product;
          Assert.NotNull(product);
          var nameProp = typeof(Product).GetProperty("Name");
          var quantityPerUnitIdProp = typeof(Product).GetProperty("QuantityPerUnitId");
          var reorderLevelProp = typeof(Product).GetProperty("ReorderLevel");
          var supplierIdProp = typeof(Product).GetProperty("SupplierId");
          var supplierProp = typeof(Product).GetProperty("Supplier");
          var unitPriceProp = typeof(Product).GetProperty("UnitPrice");
          var unitsInStockProp = typeof(Product).GetProperty("UnitsInStock");
          var unitsOnOrderProp = typeof(Product).GetProperty("UnitsOnOrder");
          var quantityPerUnitProp = typeof(Product).GetProperty("QuantityPerUnit");
          Assert.NotNull(nameProp);
          Assert.NotNull(quantityPerUnitIdProp);
          Assert.NotNull(reorderLevelProp);
          Assert.NotNull(supplierIdProp);
          Assert.NotNull(supplierProp);
          Assert.NotNull(unitPriceProp);
          Assert.NotNull(unitsInStockProp);
          Assert.NotNull(unitsOnOrderProp);
          Assert.NotNull(quantityPerUnitProp);
          nameProp.SetValue(product, "Test Product");
          quantityPerUnitIdProp.SetValue(product, 10);
          reorderLevelProp.SetValue(product, 5);
          supplierIdProp.SetValue(product, supplier.Id);
          supplierProp.SetValue(product, supplier);
          unitPriceProp.SetValue(product, 10.5m);
          unitsInStockProp.SetValue(product, 100);
          unitsOnOrderProp.SetValue(product, 20);
          quantityPerUnitProp.SetValue(product, lookup);

          // Assert
          Assert.Equal("Test Product", product.Name);
          Assert.Equal(10, product.QuantityPerUnitId);
          Assert.Equal(5, product.ReorderLevel);
          Assert.Equal(supplier, product.Supplier);
          Assert.Equal(10.5m, product.UnitPrice);
          Assert.Equal(100, product.UnitsInStock);
          Assert.Equal(20, product.UnitsOnOrder);
          Assert.Equal(lookup, product.QuantityPerUnit);
      }

     

      [Fact]
      public void Lookup_CanHaveChildren()
      {
          // Arrange
          var parent = new Lookup { Name = "Parent" };
          var child = new Lookup { Name = "Child", Parent = parent };
          parent.Children.Add(child);

          // Assert
          Assert.Single(parent.Children);
          Assert.Equal(parent, child.Parent);
      }

    }
}