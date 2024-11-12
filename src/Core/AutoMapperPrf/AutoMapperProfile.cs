

using AutoMapper;
using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.Movements.Domains.DTOs;
using GestionInventario.src.Modules.Movements.Domains.Models;
using GestionInventario.src.Modules.ProductCategories.Domain.DTOs;
using GestionInventario.src.Modules.ProductCategories.Domain.Model;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Suppliers.Domains.DTOs;
using GestionInventario.src.Modules.Suppliers.Domains.Models;
using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;
using GestionInventario.src.Modules.Users.Domains.Models;
using GestionInventario.src.Modules.Users.Domains.Models.Enums;

namespace GestionInventario.src.Core.AutoMapperPrf
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateProductMaps();
            CreateProductCategoryMaps();
            CreateCategoryMaps();
            CreateSupplierMaps();
            CreateUserMaps();
            CreateAddressMaps();
            CreateMovementMaps();
        }

        private void CreateMovementMaps()
        {
            CreateMap<Movement, MovementResponse>().ReverseMap();
        }

        private void CreateProductMaps()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            
            // Mapeo de ProductRequest a Product
            CreateMap<ProductRequest, Product>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate != null ? 
            DateOnly.Parse(src.ExpirationDate, System.Globalization.CultureInfo.InvariantCulture) : default)); // Convierte string a DateOnly

            CreateMap<Product, ProductRequest>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate.ToString())); // Convierte DateOnly a string

            // Mapeo de productResponseId a Product
            CreateMap<Product, ProductResponseId>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate.ToString())); // Convierte DateOnly a string

            CreateMap<ProductResponseId, Product>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate != null ? DateOnly.Parse(src.ExpirationDate, System.Globalization.CultureInfo.InvariantCulture) : default)); // Convierte string a DateOnly
        
            //mapeo para product con categorias
            CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ExpirationDate,
                opt => opt.MapFrom(src => src.ExpirationDate.ToString()))
            .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category.Name).ToList())
            ).ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
            CreateMap<ProductResponse, Product>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate != null ? DateOnly.Parse(src.ExpirationDate, System.Globalization.CultureInfo.InvariantCulture) : default))
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
           
        }

        private void CreateProductCategoryMaps()
        {
            CreateMap<ProductCategory, ProductCategoryRequest>().ReverseMap();
        }

        private void CreateCategoryMaps()
        {
            CreateMap<Category, CategoryResponseName>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
        }

        private void CreateSupplierMaps()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, SupplierResponse>().ReverseMap();

            CreateMap<SupplierUpdateDto, Supplier>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }

        private void CreateUserMaps()
        {
           // Mapeo de User a UserResponse, excluyendo campos de Identity que no queremos mapear manualmente
        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType == DocumentType.CC ? "CC" : "CE"))
            .ReverseMap();

        // Mapeo de User a UserRequest, excluyendo PasswordHash o cualquier otro campo que no necesitas
        CreateMap<User, UserRequest>()
            .ForMember(dest => dest.DocumentType, opt => opt.MapFrom(src => src.DocumentType == DocumentType.CC ? "CC" : "CE"))
            .ForMember(dest => dest.Password, opt => opt.Ignore()) // Excluye la contraseña de este mapeo, ya que se manejará de manera diferente
            .ReverseMap();

        // Mapeo de UserUpdateRequest a User, sin modificar campos sensibles de Identity
        CreateMap<UserUpdateRequest, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Excluye PasswordHash para evitar sobreescritura
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Condición para solo mapear valores no nulos

        }

        private void CreateAddressMaps()
        {
            CreateMap<Address, AddressResponse>().ReverseMap();
            CreateMap<Address, AddressRequest>().ReverseMap();
            
            CreateMap<Address, AddressUpdateRequest>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AddressUpdateRequest, Address>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            
        }
    }
}

