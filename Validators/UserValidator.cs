namespace GestionInventario.Validators;

using GestionInventario.Models;

public static class UserValidator
{
    public static void ValidateUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
        }

        if (string.IsNullOrWhiteSpace(user.Name))
        {
            throw new ArgumentException("El nombre es requerido.", nameof(user.Name));
        }

        if (string.IsNullOrWhiteSpace(user.Email) || !IsValidEmail(user.Email))
        {
            throw new ArgumentException("El correo electrónico es requerido y debe ser válido.", nameof(user.Email));
        }

        // Aquí puedes agregar más validaciones según sea necesario
    }

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;

        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch(FormatException)
        {
            return false;
        }
    }
}




