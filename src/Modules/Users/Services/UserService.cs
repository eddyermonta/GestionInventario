using AutoMapper;
using GestionInventario.src.Modules.Addresses.Domains.Models;
using GestionInventario.src.Modules.Addresses.Services;
using GestionInventario.src.Modules.Roles.Services;
using GestionInventario.src.Modules.Users.Domains.DTOs;
using GestionInventario.src.Modules.Users.Domains.DTOS;
using GestionInventario.src.Modules.Users.Domains.Models;
using GestionInventario.src.Modules.Users.Repositories;

namespace GestionInventario.src.Modules.Users.Services
{
    public class UserService
    ( 
        IUserRepository userRepository,
        IRoleService roleService,
        IAddressService addressService,
        IMapper mapper
    ) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IAddressService _addressService = addressService;
        private readonly IRoleService _roleService = roleService;
        private readonly IMapper _mapper = mapper;


        public async Task<UserResponse?> AddUser(UserRequest userRequest)
        {
            try
            {
                var roles = new List<string> { "ADMIN", "AUXILIAR" };
                await _roleService.EnsureRolesExist(roles);
                
                // Validar que el usuario no exista con el mismo email
                if (await GetUserByEmail(userRequest.Email) != null){
                    Console.WriteLine("User already exists with the same email.");
                    return null;
                }  
                 
                // mappear el objeto UserRequest a User y valida contraseña vacia
                var newUser = _mapper.Map<User>(userRequest);
                //asignar email como username
                newUser.UserName = userRequest.Email;

                // Crear el usuario y validar éxito         
                var createdSucceeded = await _userRepository.AddUser(newUser, userRequest.Password);
                if (!createdSucceeded){
                    throw new ArgumentException("The user could not be created.");
                }

                //asignar rol y validar éxito
                if (!await AssignRole(newUser,"ADMIN")){
                    throw new ArgumentException("The role could not be assigned.");
                }
            
                return _mapper.Map<UserResponse>(newUser);

            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }

        private async Task<bool> AssignRole(User newUser, string role){
            return await _roleService.AssingRoleToUser(newUser, role); //deberia agregar un rol
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var userList = new List<User>();
            foreach (var user in users)
            {
                user.Address = await GetAddressByUserId(user.Id);
                userList.Add(user);
            }
            users = userList;
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        private async Task <Address> GetAddressByUserId(string userId)
        {
             var addressResponse = await _addressService.GetAddressByUserId(userId);
             return _mapper.Map<Address>(addressResponse);
        }

        public async Task<UserResponse?> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return null;
            user.Address = await GetAddressByUserId(user.Id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse?> GetUserById(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return null;
            user.Address = await GetAddressByUserId(user.Id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> UpdateUser(UserUpdateRequest userUpdateRequest, string email)
        {
            var existingUser = await _userRepository.GetUserByEmail(email);
            if (existingUser == null) return false; // Indicate failure

            if(userUpdateRequest.IsActive.HasValue) existingUser.IsActive = userUpdateRequest.IsActive.Value;        
            _mapper.Map(userUpdateRequest, existingUser);
            
            var result = await _userRepository.UpdateUser(existingUser);
            return result;
        }
    }
}