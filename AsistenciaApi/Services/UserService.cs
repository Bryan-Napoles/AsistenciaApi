using AsistenciaApi.Models;
using AsistenciaApi.Data;
using AsistenciaApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace AsistenciaApi.Services
{
    public class UserService : IUserService
    {
        private readonly AsistenciaDbContext _context;

        public UserService(AsistenciaDbContext context)
        {
            _context = context;
        }

        // Método para registrar un nuevo usuario
        public async Task<UsuarioDTO> RegisterUserAsync(Usuario usuario)
        {
            // Verificar si el usuario ya existe
            var existingUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == usuario.Nombre &&
                                          u.ApellidoPaterno == usuario.ApellidoPaterno &&
                                          u.ApellidoMaterno == usuario.ApellidoMaterno);
            if (existingUser != null)
                return null;  // Usuario ya existe

            // Encriptar la contraseña
            usuario.Contrasena = EncryptPassword(usuario.Contrasena);

            // Guardar el nuevo usuario
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Retornar un UsuarioDTO
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno
            };
        }

        // Método para autenticar un usuario
        public async Task<UsuarioDTO> LoginUserAsync(string nombre, string contrasena)
        {
            // Buscar al usuario en la base de datos por su nombre
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == nombre);

            // Si no se encuentra el usuario o la contraseña no es correcta
            if (user == null || !VerifyPassword(contrasena, user.Contrasena))
                return null;  // Usuario no encontrado o contraseña incorrecta

            // Si la contraseña es correcta, retornamos un DTO con los datos del usuario
            return new UsuarioDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                ApellidoPaterno = user.ApellidoPaterno,
                ApellidoMaterno = user.ApellidoMaterno
            };
        }

        // Método para verificar las contraseñas encriptadas
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Encriptamos la contraseña proporcionada por el usuario
            var enteredHash = EncryptPassword(enteredPassword);

            // Comparamos el hash de la contraseña proporcionada con el hash almacenado
            return enteredHash == storedPassword;
        }

        // Método para encriptar la contraseña
        private string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
