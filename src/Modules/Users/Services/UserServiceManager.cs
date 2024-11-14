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
        RoleManager<IdentityRole> roleManager,
        IMapper mapper
    ) : IUserService
    {


        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IMapper _mapper = mapper;


        public async Task<UserResponse?> AddUser(UserRequest userRequest)
        {
            
            var roles = new List<string> { "ADMIN", "AUXILIAR" };

            await Task.WhenAll(roles.Select(_roleManager.RoleExistsAsync));
            
            if (await _userManager.FindByEmailAsync(userRequest.Email) != null) return null;

            // Crear el nuevo usuario y asignar roles
            var newUser = _mapper.Map<User>(userRequest);
            if (string.IsNullOrEmpty(userRequest.Password)) return null;
            var creationResult = await _userManager.CreateAsync(newUser, userRequest.Password);
            if (!creationResult.Succeeded) return null;

            // Asignar roles al usuario creado y validar éxito
            var roleResult = await _userManager.AddToRolesAsync(newUser, roles);
            if (!roleResult.Succeeded) return null;

            return _mapper.Map<UserResponse>(newUser);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await Task.Run(() => _userManager.Users.ToList()); // Devuelve todos los usuarios de la base de datos
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<UserResponse?> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
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
    

    private async Task EnsureRoleExistsAsync(string role)
    {   
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role));
         }
    }
    }
}