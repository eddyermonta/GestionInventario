using AutoMapper;
using GestionInventario.src.Users.Domains.DTOs;
using GestionInventario.src.Users.Domains.DTOS;
using GestionInventario.src.Users.Domains.Models;

namespace GestionInventario.src.AutoMapperPrf
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDto, User>()
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address != null ? new Address // Mapear Address
             {
                ZipCode = src.Address.ZipCode,
                City = src.Address.City ?? string.Empty,
                Country = src.Address.Country ?? string.Empty,
                State = src.Address.State ?? string.Empty,
                Street = src.Address.Street ?? string.Empty 
             } : null )
             ); 

            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressDto // Mapear Address
            {
                ZipCode = src.Address.ZipCode,
                City = src.Address.City,
                Country = src.Address.Country,
                State = src.Address.State,
                Street = src.Address.Street
            }
            ));

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
                    opt.MapFrom(src => src.Address != null && 
                        (!string.IsNullOrEmpty(src.Address.Street) || 
                        !string.IsNullOrEmpty(src.Address.City) || 
                        !string.IsNullOrEmpty(src.Address.State) || 
                        !string.IsNullOrEmpty(src.Address.Country) || 
                        src.Address.ZipCode != 0) 
                        ? new Address
                        {
                            ZipCode = src.Address.ZipCode ?? 0,
                            City = src.Address.City ?? string.Empty,
                            Country = src.Address.Country ?? string.Empty,
                            State = src.Address.State ?? string.Empty,
                            Street = src.Address.Street ?? string.Empty
                        } : null));   
        }
    }
}
