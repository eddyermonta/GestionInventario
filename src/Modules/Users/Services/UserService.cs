using AutoMapper;
using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;
using GestionInventario.src.Modules.Users.Domains.Models;
using GestionInventario.src.Modules.Users.Repositories;

namespace GestionInventario.src.Modules.Users.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        public void AddUser(UserDto userDto)
        {
            if (_userRepository.GetUserByEmail(userDto.Email) != null) throw new InvalidOperationException("El usuario ya existe.");
             
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password); // Hashea la contraseña
            _userRepository.AddUser(user);
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto? GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return null;

            return _mapper.Map<UserDto>(user);
        }

        public bool UpdateUser(UserUpdateDto userUpdateDto, string email)
        {
            var existingUser = _userRepository.GetUserByEmail(email);
            if (existingUser == null) return false; // Indicate failure

            _mapper.Map(userUpdateDto, existingUser);
            if (!string.IsNullOrEmpty(userUpdateDto.Password))
                 existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.Password);
            
            _userRepository.UpdateUser(existingUser);

            return true;
        }
    }

}