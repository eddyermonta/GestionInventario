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
        public UserResponse? AddUser(UserRequest userRequest)
        {
            if (_userRepository.GetUserByEmail(userRequest.Email) != null) return null; // Indicate failure
            var user = _mapper.Map<User>(userRequest);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRequest.Password); // Hashea la contraseña
            _userRepository.AddUser(user);
            return _mapper.Map<UserResponse>(user);
        }

        public IEnumerable<UserResponse> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public UserResponse? GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return null;

            return _mapper.Map<UserResponse>(user);
        }

        public bool UpdateUser(UserUpdateRequest userUpdateRequest, string email)
        {
            var existingUser = _userRepository.GetUserByEmail(email);
            if (existingUser == null) return false; // Indicate failure
            

            var userUpdated = _mapper.Map(userUpdateRequest, existingUser);
            
            if (!string.IsNullOrEmpty(userUpdateRequest.Password)) 
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateRequest.Password);     
            
            _userRepository.UpdateUser(userUpdated);
            return true;
        }
    }

}