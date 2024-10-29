using AutoMapper;
using GestionInventario.src.Users.Domains.DTOs;
using GestionInventario.src.Users.Domains.DTOS;
using GestionInventario.src.Users.Domains.Models;
using GestionInventario.src.Users.Repositories;

namespace GestionInventario.src.Users.Services
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

        public bool UpdateUser(UserUpdateDto userDto, string email)
        {
            var existingUser = _userRepository.GetUserByEmail(email);
            if (existingUser == null) return false; // Indicate failure

            // Actualizamos parcialmente solo los campos necesarios
            UpdatePartialUser(existingUser, userDto);

            _userRepository.UpdateUser(existingUser);
            return true;
        }

       private void UpdatePartialUser(User existingUser, UserUpdateDto userDto)
       {
        // Utilizamos AutoMapper para mapear los campos que se proporcionaron en el DTO
        _mapper.Map(userDto, existingUser);

        // Aquí puedes mantener la lógica de actualización condicional que tenías
        if (userDto.Address != null)
        {
            if (existingUser.Address != null)
            {
                // Actualizamos los campos de la dirección solo si se proporcionan
                if (!string.IsNullOrEmpty(userDto.Address.Street))
                    existingUser.Address.Street = userDto.Address.Street;

                if (!string.IsNullOrEmpty(userDto.Address.City))
                    existingUser.Address.City = userDto.Address.City;

                // Otros campos de la dirección se pueden actualizar de manera similar
            }
            else
            {
                // Si no tiene dirección, la asignamos
                existingUser.Address = _mapper.Map<Address>(userDto.Address);
            }
        }

        // Aquí se mantiene la actualización de la contraseña si es proporcionada
        if (!string.IsNullOrEmpty(userDto.Password)) 
        {
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
        }
    }

    }
}