

using AutoMapper;
using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Domain.Models;
using GestionInventario.src.Modules.ProductCategories.Domain.DTOs;
using GestionInventario.src.Modules.ProductCategories.Domain.Model;
using GestionInventario.src.Modules.Products.Domain.DTOs;
using GestionInventario.src.Modules.Products.Domain.Models;
using GestionInventario.src.Modules.Suppliers.Domains.DTOs;
using GestionInventario.src.Modules.Suppliers.Domains.Models;
using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;
using GestionInventario.src.Modules.Users.Domains.Models;

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
        }

        private void CreateProductMaps()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            
            // Mapeo de ProductRequest a Product
            CreateMap<ProductRequest, Product>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate != null ? DateOnly.Parse(src.ExpirationDate, System.Globalization.CultureInfo.InvariantCulture) : default)); // Convierte string a DateOnly

            CreateMap<Product, ProductRequest>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate.ToString())); // Convierte DateOnly a string

            // Mapeo de ProductRequestDto a Product
            CreateMap<Product, ProductRequestDto>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate.ToString())); // Convierte DateOnly a string

            CreateMap<ProductRequestDto, Product>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate != null ? DateOnly.Parse(src.ExpirationDate, System.Globalization.CultureInfo.InvariantCulture) : default)); // Convierte string a DateOnly
        
            //mapeo para product con categorias
            CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate.ToString()))
            .ForMember(dest => dest.Categories,
            opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category.Name).ToList()));
            
            CreateMap<ProductResponse, Product>()
            .ForMember(dest => dest.ExpirationDate,
            opt => opt.MapFrom(src => src.ExpirationDate != null ? DateOnly.Parse(src.ExpirationDate, System.Globalization.CultureInfo.InvariantCulture) : default));
        
            //mapeo para productUPDATE
            CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.Name, opt =>
            {
                opt.PreCondition(src => !string.IsNullOrEmpty(src.Name));
                opt.MapFrom(src => src.Name);
            })
            .ForMember(dest => dest.Description, opt =>
            {
                opt.PreCondition(src => !string.IsNullOrEmpty(src.Description));
                opt.MapFrom(src => src.Description);
            })
            .ForMember(dest => dest.Amount, opt =>
            {
                opt.PreCondition(src => src.Amount > 0);
                opt.MapFrom(src => src.Amount);
            })
            .ForMember(dest => dest.UnitPrice, opt =>
            {
                opt.PreCondition(src => src.UnitPrice > 0);
                opt.MapFrom(src => src.UnitPrice);
            })
            .ForMember(dest => dest.ExpirationDate, opt =>
                opt.MapFrom(src => !string.IsNullOrEmpty(src.ExpirationDate) ? DateOnly.Parse(src.ExpirationDate, System.Globalization.CultureInfo.InvariantCulture) : default))
            .ForMember(dest => dest.Weight, opt =>
            {
                opt.PreCondition(src => src.Weight != null);
                opt.MapFrom(src => src.Weight);
            });
        }

        private void CreateProductCategoryMaps()
        {
            CreateMap<ProductCategory, ProductCategoryRequest>().ReverseMap();
        }

        private void CreateCategoryMaps()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryGet>().ReverseMap();
        }

        private void CreateSupplierMaps()
        {
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, SupplierGetElementDto>().ReverseMap();

            CreateMap<SupplierUpdateDto, Supplier>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Name));
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.Phone, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Phone));
                    opt.MapFrom(src => src.Phone);
                })
                .ForMember(dest => dest.Email, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Email));
                    opt.MapFrom(src => src.Email);
                })
                .ForMember(dest => dest.Address, opt =>
                {
                    opt.PreCondition(src => src.Address != null &&
                        (!string.IsNullOrEmpty(src.Address.Street) ||
                        !string.IsNullOrEmpty(src.Address.City) ||
                        !string.IsNullOrEmpty(src.Address.State) ||
                        !string.IsNullOrEmpty(src.Address.Country) ||
                        src.Address.ZipCode != 0));
                    opt.MapFrom(src => src.Address != null ? new AddressUpdateDto
                    {
                        ZipCode = src.Address.ZipCode ?? 0,
                        City = src.Address.City ?? string.Empty,
                        Country = src.Address.Country ?? string.Empty,
                        State = src.Address.State ?? string.Empty,
                        Street = src.Address.Street ?? string.Empty
                    } : new AddressUpdateDto());
                });
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Name));
                    opt.MapFrom(src => src.Name);
                })
                .ForMember(dest => dest.LastName, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.LastName));
                    opt.MapFrom(src => src.LastName);
                })
                .ForMember(dest => dest.DocumentNumber, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.DocumentNumber));
                    opt.MapFrom(src => src.DocumentNumber);
                })
                .ForMember(dest => dest.PhoneNumber, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.PhoneNumber));
                    opt.MapFrom(src => src.PhoneNumber);
                })
                .ForMember(dest => dest.IsActive, opt => 
                    opt.MapFrom(src => src.IsActive)) // Asignamos directamente ya que IsActive es bool
                .ForMember(dest => dest.Address, opt => 
                {
                    opt.PreCondition(src => src.Address != null &&
                        (!string.IsNullOrEmpty(src.Address.Street) ||
                        !string.IsNullOrEmpty(src.Address.City) ||
                        !string.IsNullOrEmpty(src.Address.State) ||
                        !string.IsNullOrEmpty(src.Address.Country) ||
                        src.Address.ZipCode != 0));
                    opt.MapFrom(src => src.Address != null ? new AddressUpdateDto
                    {
                        ZipCode = src.Address.ZipCode ?? 0,
                        City = src.Address.City ?? string.Empty,
                        Country = src.Address.Country ?? string.Empty,
                        State = src.Address.State ?? string.Empty,
                        Street = src.Address.Street ?? string.Empty
                    } : new AddressUpdateDto());
                });
        }

        private void CreateAddressMaps()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressGetElementDto>().ReverseMap();
        }
    }
}

