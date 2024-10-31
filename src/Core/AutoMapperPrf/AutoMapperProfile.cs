

using AutoMapper;
using GestionInventario.src.Modules.Categories.Domain.DTOs;
using GestionInventario.src.Modules.Categories.Domain.Models;
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
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Supplier, SupplierDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();

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


            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductCategoryDto, ProductCategoryDto>().ReverseMap();

            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();

        }
    }
}

