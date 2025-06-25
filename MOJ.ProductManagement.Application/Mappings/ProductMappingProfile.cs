using AutoMapper;
using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Application.DTOs.Supplier;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Entity to DTO
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.QuantityPerUnitName, opt => opt.MapFrom(src => src.QuantityPerUnit.Name));

            //DTO to Entity
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Supplier, opt => opt.Ignore())
                .ForMember(dest => dest.QuantityPerUnit, opt => opt.Ignore());


            //Supplier
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<UpdateSupplierDto, Supplier>();

        }
    }
}
