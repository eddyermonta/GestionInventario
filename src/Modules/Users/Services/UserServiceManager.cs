using AutoMapper;
using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;
using GestionInventario.src.Modules.Users.Domains.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionInventario.src.Modules.Users.Services
{
    public class UserServiceManager
    ( 
        UserManager<User> userManager,
        IMapper mapper

    ) : IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IMapper _mapper = mapper;
        public UserResponse? AddUser(UserRequest userRequest)
        {
            
            var existingUser =  _userManager.FindByEmailAsync(userRequest.Email);
            if (existingUser.AsyncState != null) return null; // Indicate failure
           

            var user = _mapper.Map<User>(userRequest);
            
            if (string.IsNullOrEmpty(userRequest.Password)) return null; // Indicate failure
            
            var result = _userManager.CreateAsync(user, userRequest.Password);
            if (!result.IsCompletedSuccessfully) return null;  
            
            return _mapper.Map<UserResponse>(user);
        }

        public IEnumerable<UserResponse> GetAllUsers()
        {
            var users = _userManager.Users.ToList(); // Devuelve todos los usuarios de la base de datos
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public UserResponse? GetUserByEmail(string email)
        {
             var user =  _userManager.FindByEmailAsync(email);
            if (user == null) return null;
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> UpdateUser(UserUpdateRequest userUpdateRequest, string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser == null) return false; // Indicate failure

            _mapper.Map(userUpdateRequest, existingUser);

            var result =  _userManager.UpdateAsync(existingUser);
            return result.IsCompletedSuccessfully;
        }
    }
}